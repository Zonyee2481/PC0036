// ***********************************************************************
// Module           : eSCLLibHelper for C#
// Author           : Lei Youbing
// Created          : 2017-01-03
//
// Last Modified By : Lei Youbing
// Last Modified On : 2018-06-22
// ***********************************************************************
//     Copyright (c) Shanghai AMP & MOONS' Automation Co., Ltd.. All rights reserved.
// ***********************************************************************

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace MotionIODevice
{
	public class eSCLLibHelper
	{
		public struct CommandInfo
		{
			public int Count;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
			public byte[] Values;
		}

		public struct ErrorInfo
		{
			public int ErrorCode;
			public string Command;
			public string ErrorMessage;
		}

        private const string DLL_FILENAME = "eSCLLib_x86.dll";   // for 64-bit windows, please comment this line and uncomment next line
        //private const string DLL_FILENAME = "eSCLLib_x64.dll";   // for 32-bit windows,, please comment this line and uncomment previous line

        #region Private Methods

        public delegate void CSCallback();

		[DllImport(DLL_FILENAME, EntryPoint = "OnDataSend", CharSet = CharSet.Ansi)]
		private static extern void _OnDataSend(CSCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "OnDataReceive", CharSet = CharSet.Ansi)]
		private static extern void _OnDataReceive(CSCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "OnConnect", CharSet = CharSet.Ansi)]
		private static extern void _OnConnect(CSCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "OnClose", CharSet = CharSet.Ansi)]
		private static extern void _OnClose(CSCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "Open", CharSet = CharSet.Ansi)]
		private static extern bool _Open(bool bTCPIP);

		[DllImport(DLL_FILENAME, EntryPoint = "Close", CharSet = CharSet.Ansi)]
		private static extern bool _Close();

		[DllImport(DLL_FILENAME, EntryPoint = "ClearInputBuffer")]
		public static extern bool _ClearInputBuffer(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "Ping", CharSet = CharSet.Ansi)]
		private static extern bool _Ping(ushort nNodeID, ref int nBuildNo, ref IntPtr pMACID);

		[DllImport(DLL_FILENAME, EntryPoint = "WakeUp", CharSet = CharSet.Ansi)]
		private static extern bool _WakeUp(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "SetCommParam", CharSet = CharSet.Ansi)]
		private static extern bool _SetCommParam(bool bSave);

		[DllImport(DLL_FILENAME, EntryPoint = "IsOpen", CharSet = CharSet.Ansi)]
		private static extern bool _IsOpen();

		[DllImport(DLL_FILENAME, EntryPoint = "IsOpenByNodeID", CharSet = CharSet.Ansi)]
		private static extern bool _IsOpenByNodeID(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "SetExecuteTimeOut", CharSet = CharSet.Ansi)]
		private static extern void _SetExecuteTimeOut(uint nExecuteTimeOut);

		[DllImport(DLL_FILENAME, EntryPoint = "GetExecuteTimeOut", CharSet = CharSet.Ansi)]
		private static extern uint _GetExecuteTimeOut();

		[DllImport(DLL_FILENAME, EntryPoint = "SetExecuteRetryTimes", CharSet = CharSet.Ansi)]
		private static extern void _SetExecuteRetryTimes(byte nExecuteRetryTimes);

		[DllImport(DLL_FILENAME, EntryPoint = "GetExecuteRetryTimes", CharSet = CharSet.Ansi)]
		private static extern byte _GetExecuteRetryTimes();

		[DllImport(DLL_FILENAME, EntryPoint = "SendSCLCommand", CharSet = CharSet.Ansi)]
		private static extern bool _SendSCLCommand(ushort nNodeID, byte[] arrCommand);

		[DllImport(DLL_FILENAME, EntryPoint = "ExecuteSCLCommand", CharSet = CharSet.Ansi)]
		private static extern bool _ExecuteSCLCommand(ushort nNodeID, byte[] arrCommand, ref IntPtr ptrResponse);

		[DllImport(DLL_FILENAME, EntryPoint = "SetTriggerSendEvent", CharSet = CharSet.Ansi)]
		private static extern void _SetTriggerSendEvent(bool bTriggerSendEvent);

		[DllImport(DLL_FILENAME, EntryPoint = "SetTriggerReceiveEvent", CharSet = CharSet.Ansi)]
		private static extern void _SetTriggerReceiveEvent(bool bTriggerReceiveEvent);

		[DllImport(DLL_FILENAME, EntryPoint = "SetTCPAutoReconnect", CharSet = CharSet.Ansi)]
		private static extern void _SetTCPAutoReconnect(bool bTCPAutoReconnect);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastCommandSent", CharSet = CharSet.Ansi)]
		private static extern bool _GetLastCommandSent(ushort nNodeID, IntPtr ptrCommandStruct);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastCommandReceived", CharSet = CharSet.Ansi)]
		private static extern bool _GetLastCommandReceived(ushort nNodeID, IntPtr ptrCommandStruct);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastErrorInfo", CharSet = CharSet.Ansi)]
		private static extern void _GetLastErrorInfo(ref ErrorInfo errorInfo);

		[DllImport(DLL_FILENAME, EntryPoint = "GetAxesCount", CharSet = CharSet.Ansi)]
		private static extern ushort _GetAxesCount();

		[DllImport(DLL_FILENAME, EntryPoint = "AddAxis", CharSet = CharSet.Ansi)]
		private static extern bool _AddAxis(string strIPAddress);

		[DllImport(DLL_FILENAME, EntryPoint = "ClearAllAxes", CharSet = CharSet.Ansi)]
		private static extern void _ClearAllAxes();

		[DllImport(DLL_FILENAME, EntryPoint = "GetIPAddressListString", CharSet = CharSet.Ansi)]
		private static extern void _GetIPAddressListString(ref ushort nCount, ref IntPtr strNodeIDMapping);

		// Advanced APIs

		[DllImport(DLL_FILENAME, EntryPoint = "DriveEnable")]
		public static extern bool _DriveEnable(ushort nNodeID, bool bEnable);

		[DllImport(DLL_FILENAME, EntryPoint = "SetDriveOutput")]
		public static extern bool _SetDriveOutput(ushort nNodeID, byte nOutput1, char chOutputStatus1, IntPtr nOutput2, IntPtr chOutputStatus2, IntPtr nOutput3, IntPtr chOutputStatus3, IntPtr nOutput4, IntPtr chOutputStatus4, IntPtr nOutput5, IntPtr chOutputStatus5, IntPtr nOutput6, IntPtr chOutputStatus6);

		[DllImport(DLL_FILENAME, EntryPoint = "SetP2PProfile")]
		public static extern bool _SetP2PProfile(ushort nNodeID, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "SetJogProfile")]
		public static extern bool _SetJogProfile(ushort nNodeID, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "RelMove")]
		public static extern bool _RelMove(ushort nNodeID, int nDistance, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "AbsMove")]
		public static extern bool _AbsMove(ushort nNodeID, int nDistance, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "FeedtoSensor")]
		public static extern bool _FeedtoSensor(ushort nNodeID, IntPtr nDistance, byte nInputSensor, char chInputStatus, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "P2PMoveWithVelocityChange")]
		public static extern bool _P2PMoveWithVelocityChange(ushort nNodeID, IntPtr nDistance1, IntPtr nDistance2, IntPtr nInputSensor, IntPtr chInputStatus, IntPtr dVelocity1, IntPtr dVelocity2, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "P2PMoveAndSetOutput", CharSet = CharSet.Ansi)]
		private static extern bool _P2PMoveAndSetOutput(ushort nNodeID, IntPtr nMoveDistance, IntPtr nSetOutputDistance, byte nOutput, char chOutputStatus, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "FeedtoDoubleSensor", CharSet = CharSet.Ansi)]
		private static extern bool _FeedtoDoubleSensor(ushort nNodeID, byte nInputSensor1, char chInputStatus1, byte nInputSensor2, char chInputStatus2, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "FeedtoSensorWithMaskDistance", CharSet = CharSet.Ansi)]
		private static extern bool _FeedtoSensorWithMaskDistance(ushort nNodeID, IntPtr nStopDistance, IntPtr nMaskDistance, byte nInputSensor, char chInputStatus, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "SeekHome", CharSet = CharSet.Ansi)]
		private static extern bool _SeekHome(ushort nNodeID, byte nInputSensor, char chInputStatus, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "ExtendedSeekHome", CharSet = CharSet.Ansi)]
		private static extern bool _ExtendedSeekHome(ushort nNodeID, byte nInputSensor, char chInputStatus, IntPtr dVelocity1, IntPtr dVelocity2, IntPtr dVelocity3, IntPtr dAccel1, IntPtr dAccel2, IntPtr dAccel3, IntPtr dDecel1, IntPtr dDecel2, IntPtr dDecel3);

		[DllImport(DLL_FILENAME, EntryPoint = "HardStopHoming", CharSet = CharSet.Ansi)]
		private static extern bool _HardStopHoming(int nNodeID, bool bWithIndex, IntPtr dVelocity1, IntPtr dVelocity2, IntPtr dVelocity3, IntPtr dAccel1, IntPtr dAccel2, IntPtr dAccel3, IntPtr dDecel1, IntPtr dDecel2, IntPtr dDecel3);

		[DllImport(DLL_FILENAME, EntryPoint = "IsMotorEnabled")]
		public static extern bool _IsMotorEnabled(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsSampling")]
		public static extern bool _IsSampling(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInFault")]
		public static extern bool _IsInFault(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInPosition")]
		public static extern bool _IsInPosition(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsMoving")]
		public static extern bool _IsMoving(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsJogging")]
		public static extern bool _IsJogging(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsStopping")]
		public static extern bool _IsStopping(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsWaitingforInput")]
		public static extern bool _IsWaitingforInput(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsSavingParam")]
		public static extern bool _IsSavingParam(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarm")]
		public static extern bool _IsInAlarm(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsHoming")]
		public static extern bool _IsHoming(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsWaitingforTime")]
		public static extern bool _IsWaitingforTime(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsRunningWizard")]
		public static extern bool _IsRunningWizard(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsCheckingEncoder")]
		public static extern bool _IsCheckingEncoder(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsRunningQProgram")]
		public static extern bool _IsRunningQProgram(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInitializingOrServoReady")]
		public static extern bool _IsInitializingOrServoReady(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmPositionLimit")]
		public static extern bool _IsInAlarmPositionLimit(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmCWLimit")]
		public static extern bool _IsInAlarmCWLimit(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmCCWLimit")]
		public static extern bool _IsInAlarmCCWLimit(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmOverTemp")]
		public static extern bool _IsInAlarmOverTemp(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmOverVoltage")]
		public static extern bool _IsInAlarmOverVoltage(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmUnderVoltage")]
		public static extern bool _IsInAlarmUnderVoltage(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmOverCurrent")]
		public static extern bool _IsInAlarmOverCurrent(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmEncoderFault")]
		public static extern bool _IsInAlarmEncoderFault(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmCommError")]
		public static extern bool _IsInAlarmCommError(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmBadFlash")]
		public static extern bool _IsInAlarmBadFlash(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmBlankQSegment")]
		public static extern bool _IsInAlarmBlankQSegment(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmMoveWhileDisabledMSeries")]
		public static extern bool _IsInAlarmMoveWhileDisabledMSeries(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmACPowerPhasseLostMSeries")]
		public static extern bool _IsInAlarmACPowerPhasseLostMSeries(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmSafeTorqueOffMSeries")]
		public static extern bool _IsInAlarmSafeTorqueOffMSeries(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmVelocityLimitMSeries")]
		public static extern bool _IsInAlarmVelocityLimitMSeries(ushort nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "IsInAlarmVoltageWarningMSeries")]
		public static extern bool _IsInAlarmVoltageWarningMSeries(ushort nNodeID);

		// AC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAccelerationRate", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAccelerationRate(ushort nNodeID, ref double dAccel);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAccelerationRate", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAccelerationRate(ushort nNodeID, double dAccel);

		// AD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogDeadband", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogDeadband(ushort nNodeID, ref byte analogDeadband);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogDeadband", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogDeadband(ushort nNodeID, byte analogDeadband);

		// AD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogDeadbandWithChannel", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogDeadbandWithChannel(ushort nNodeID, byte nAnalogChannel, ref byte analogDeadband);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogDeadbandWithChannel", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogDeadbandWithChannel(ushort nNodeID, byte nAnalogChannel, byte analogDeadband);

		// AF
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogFilter", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogFilter(ushort nNodeID, ref int nAnalogFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogFilter", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogFilter(ushort nNodeID, int nAnalogFilter);
		// AG
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogVelocityGain", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogVelocityGain(ushort nNodeID, ref int nAnalogVelocityGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogVelocityGain", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogVelocityGain(ushort nNodeID, int nAnalogVelocityGain);

		// AI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAlarmResetInput", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAlarmResetInput(ushort nNodeID, ref byte nAlarmResetInput);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAlarmResetInput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAlarmResetInput(ushort nNodeID, byte nAlarmResetInput);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadAlarmResetInputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAlarmResetInputFlexIO(ushort nNodeID, ref byte nInputUsage, ref byte nInputSensor);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAlarmResetInputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAlarmResetInputFlexIO(ushort nNodeID, byte nInputUsage, byte nInputSensor);

		// AL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAlarmCode", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAlarmCode(ushort nNodeID, ref int nAlarmCode);

		// AL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAlarmCodeWithChannel", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAlarmCodeWithChannel(ushort nNodeID, byte nChannel, ref int nAlarmCode);

		// AM
		[DllImport(DLL_FILENAME, EntryPoint = "ReadMaxAcceleration", CharSet = CharSet.Ansi)]
		private static extern bool _ReadMaxAcceleration(ushort nNodeID, ref double dMaxAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMaxAcceleration", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMaxAcceleration(ushort nNodeID, double dMaxAcceleration);

		// AN
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogTorqueGain", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogTorqueGain(ushort nNodeID, ref double nAnalogTorqueGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogTorqueGain", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogTorqueGain(ushort nNodeID, double nAnalogTorqueGain);

		// AO
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAlarmOutput", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAlarmOutput(ushort nNodeID, ref byte nOutputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAlarmOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAlarmOutput(ushort nNodeID, byte nOutputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadAlarmOutputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAlarmOutputFlexIO(ushort nNodeID, ref byte nOutputUsage, ref byte nOutput);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAlarmOutputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAlarmOutputFlexIO(ushort nNodeID, byte nOutputUsage, byte nOutput);

		// AP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogPositionGain", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogPositionGain(ushort nNodeID, ref int nPositionGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogPositionGain", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogPositionGain(ushort nNodeID, int nPositionGain);

		// AR
		[DllImport(DLL_FILENAME, EntryPoint = "WriteAlarmReset", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAlarmReset(ushort nNodeID, bool bImmediately);

		// AS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogScaling", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogScaling(ushort nNodeID, ref byte scaling);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogScaling", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogScaling(ushort nNodeID, byte scaling);

		// AT
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogThreshold", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogThreshold(ushort nNodeID, ref double dAnalogThreshold);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogThreshold", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogThreshold(ushort nNodeID, double dAnalogThreshold);

		// AV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogOffset", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogOffset(ushort nNodeID, ref double dAnalogOffset);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogOffset", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogOffset(ushort nNodeID, double dAnalogOffset);

		// AV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogOffsetMSeries", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAnalogOffsetMSeries(ushort nNodeID, byte nAnalogChannel, ref double dAnalogOffset);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogOffsetMSeries", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogOffsetMSeries(ushort nNodeID, byte nAnalogChannel, double dAnalogOffset);

		// AZ
		[DllImport(DLL_FILENAME, EntryPoint = "WriteAnalogZero", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAnalogZero(ushort nNodeID);

		// BD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadBrakeDisengageDelay", CharSet = CharSet.Ansi)]
		private static extern bool _ReadBrakeDisengageDelay(ushort nNodeID, ref double dBrakeDisengageDelay);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteBrakeDisengageDelay", CharSet = CharSet.Ansi)]
		private static extern bool _WriteBrakeDisengageDelay(ushort nNodeID, double dBrakeDisengageDelay);

		// BE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadBrakeEngageDelay", CharSet = CharSet.Ansi)]
		private static extern bool _ReadBrakeEngageDelay(ushort nNodeID, ref double dBrakeEngageDelay);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteBrakeEngageDelay", CharSet = CharSet.Ansi)]
		private static extern bool _WriteBrakeEngageDelay(ushort nNodeID, double dBrakeEngageDelay);

		// BO
		[DllImport(DLL_FILENAME, EntryPoint = "ReadBrakeOutput", CharSet = CharSet.Ansi)]
		private static extern bool _ReadBrakeOutput(ushort nNodeID, ref byte nOutputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteBrakeOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteBrakeOutput(ushort nNodeID, byte nOutputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadBrakeOutputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _ReadBrakeOutputFlexIO(ushort nNodeID, ref byte nOutputUsage, ref byte nOutput);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteBrakeOutputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _WriteBrakeOutputFlexIO(ushort nNodeID, byte nOutputUsage, byte nOutput);

		// BS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadBufferStatus", CharSet = CharSet.Ansi)]
		private static extern bool _ReadBufferStatus(ushort nNodeID, ref byte bufferStatus);

		// CA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadChangeAccelerationCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadChangeAccelerationCurrent(ushort nNodeID, ref double dChangeAccelerationCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteChangeAccelerationCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WriteChangeAccelerationCurrent(ushort nNodeID, double dChangeAccelerationCurrent);

		// CC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadChangeCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadChangeCurrent(ushort nNodeID, ref double dChangeCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteChangeCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WriteChangeCurrent(ushort nNodeID, double dChangeCurrent);

		// CD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadIdleCurrentDelayTime", CharSet = CharSet.Ansi)]
		private static extern bool _ReadIdleCurrentDelayTime(ushort nNodeID, ref double dIdleCurrentDelayTime);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteIdleCurrentDelayTime", CharSet = CharSet.Ansi)]
		private static extern bool _WriteIdleCurrentDelayTime(ushort nNodeID, double dIdleCurrentDelayTime);

		// CE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadCommunicationError", CharSet = CharSet.Ansi)]
		private static extern bool _ReadCommunicationError(ushort nNodeID, ref int nCommunicationError);

		// CF
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAntiResonanceFilterFreq", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAntiResonanceFilterFreq(ushort nNodeID, ref int nAntiResonanceFilterFreq);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAntiResonanceFilterFreq", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAntiResonanceFilterFreq(ushort nNodeID, int nAntiResonanceFilterFreq);

		// CG
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAntiResonanceFilterGain", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAntiResonanceFilterGain(ushort nNodeID, ref int nAntiResonanceFilterGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAntiResonanceFilterGain", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAntiResonanceFilterGain(ushort nNodeID, int nAntiResonanceFilterGain);

		// CI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadChangeIdleCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadChangeIdleCurrent(ushort nNodeID, ref double dChangeIdleCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteChangeIdleCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WriteChangeIdleCurrent(ushort nNodeID, double dChangeIdleCurrent);

		// CJ
		[DllImport(DLL_FILENAME, EntryPoint = "WriteCommenceJogging", CharSet = CharSet.Ansi)]
		private static extern bool _WriteCommenceJogging(ushort nNodeID);

		// CM
		[DllImport(DLL_FILENAME, EntryPoint = "ReadCommandMode", CharSet = CharSet.Ansi)]
		private static extern bool _ReadCommandMode(ushort nNodeID, ref byte nCommandMode);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteCommandMode", CharSet = CharSet.Ansi)]
		private static extern bool _WriteCommandMode(ushort nNodeID, byte nCommandMode);

		// CN
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSecondaryCommandMode", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSecondaryCommandMode(ushort nNodeID, ref byte nSecondaryCommandMode);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSecondaryCommandMode", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSecondaryCommandMode(ushort nNodeID, byte nSecondaryCommandMode);

		// CP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadChangePeakCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadChangePeakCurrent(ushort nNodeID, ref double dChangePeakCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteChangePeakCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WriteChangePeakCurrent(ushort nNodeID, double dChangePeakCurrent);

		// CR
		[DllImport(DLL_FILENAME, EntryPoint = "WriteCompareRegisters", CharSet = CharSet.Ansi)]
		private static extern bool _WriteCompareRegisters(ushort nNodeID, char chRegister1, char chRegister2);

		// CS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadChangeSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _ReadChangeSpeed(ushort nNodeID, ref double dChangeSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteChangeSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _WriteChangeSpeed(ushort nNodeID, double dChangeSpeed);

		// DC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadChangeDistance", CharSet = CharSet.Ansi)]
		private static extern bool _ReadChangeDistance(ushort nNodeID, ref int nChangeDistance);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteChangeDistance", CharSet = CharSet.Ansi)]
		private static extern bool _WriteChangeDistance(ushort nNodeID, int nChangeDistance);

		// DD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadDefaultDisplay", CharSet = CharSet.Ansi)]
		private static extern bool _ReadDefaultDisplay(ushort nNodeID, ref int nDefaultDisplay);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDefaultDisplay", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDefaultDisplay(ushort nNodeID, int nDefaultDisplay);

		// DE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadDecelerationRate", CharSet = CharSet.Ansi)]
		private static extern bool _ReadDecelerationRate(ushort nNodeID, ref double dDeceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDecelerationRate", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDecelerationRate(ushort nNodeID, double dDeceleration);

		// DI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadDistanceOrPosition", CharSet = CharSet.Ansi)]
		private static extern bool _ReadDistanceOrPosition(ushort nNodeID, ref int nDistance);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDistanceOrPosition", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDistanceOrPosition(ushort nNodeID, int nDistance);

		// DL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadDefineLimits", CharSet = CharSet.Ansi)]
		private static extern bool _ReadDefineLimits(ushort nNodeID, ref byte nDefineLimits);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDefineLimits", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDefineLimits(ushort nNodeID, byte nDefineLimits);

		// DP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadDumpingPower", CharSet = CharSet.Ansi)]
		private static extern bool _ReadDumpingPower(ushort nNodeID, ref int nDumpingPower);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDumpingPower", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDumpingPower(ushort nNodeID, int nDumpingPower);

		// DR
		[DllImport(DLL_FILENAME, EntryPoint = "WriteDataRegisterforCapture", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDataRegisterforCapture(ushort nNodeID, char chDataRegisterforCapture);

		// DS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSwitchingElectronicGearing", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSwitchingElectronicGearing(ushort nNodeID, ref byte nSwitchingElectronicGearing);

		// DS
		[DllImport(DLL_FILENAME, EntryPoint = "WriteSwitchingElectronicGearing", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSwitchingElectronicGearing(ushort nNodeID, byte nSwitchingElectronicGearing);

		// ED
		[DllImport(DLL_FILENAME, EntryPoint = "ReadEncoderDirection", CharSet = CharSet.Ansi)]
		private static extern bool _ReadEncoderDirection(ushort nNodeID, ref byte nEncoderDirection);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteEncoderDirection", CharSet = CharSet.Ansi)]
		private static extern bool _WriteEncoderDirection(ushort nNodeID, byte nEncoderDirection);

		// EF
		[DllImport(DLL_FILENAME, EntryPoint = "ReadEncoderFunction", CharSet = CharSet.Ansi)]
		private static extern bool _ReadEncoderFunction(ushort nNodeID, ref byte nEncoderFunction);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteEncoderFunction", CharSet = CharSet.Ansi)]
		private static extern bool _WriteEncoderFunction(ushort nNodeID, byte nEncoderFunction);

		// EG
		[DllImport(DLL_FILENAME, EntryPoint = "ReadElectronicGearing", CharSet = CharSet.Ansi)]
		private static extern bool _ReadElectronicGearing(ushort nNodeID, ref int nElectronicGearing);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteElectronicGearing", CharSet = CharSet.Ansi)]
		private static extern bool _WriteElectronicGearing(ushort nNodeID, int nElectronicGearing);

		// EH - Write
		[DllImport(DLL_FILENAME, EntryPoint = "WriteExtendedHoming", CharSet = CharSet.Ansi)]
		private static extern bool _WriteExtendedHoming(int nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// EI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadInputNoiseFilter", CharSet = CharSet.Ansi)]
		private static extern bool _ReadInputNoiseFilter(ushort nNodeID, ref byte nInputNoiseFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteInputNoiseFilter", CharSet = CharSet.Ansi)]
		private static extern bool _WriteInputNoiseFilter(ushort nNodeID, byte nInputNoiseFilter);

		// EN
		[DllImport(DLL_FILENAME, EntryPoint = "ReadElectronicGearingRatioNumerator", CharSet = CharSet.Ansi)]
		private static extern bool _ReadElectronicGearingRatioNumerator(ushort nNodeID, ref int nElectronicGearingRatioNumerator);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteElectronicGearingRatioNumerator", CharSet = CharSet.Ansi)]
		private static extern bool _WriteElectronicGearingRatioNumerator(ushort nNodeID, int nElectronicGearingRatioNumerator);

		// EP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadEncoderPosition", CharSet = CharSet.Ansi)]
		private static extern bool _ReadEncoderPosition(ushort nNodeID, ref int nEncoderPosition);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteEncoderPosition", CharSet = CharSet.Ansi)]
		private static extern bool _WriteEncoderPosition(ushort nNodeID, int nEncoderPosition);

		// ER
		[DllImport(DLL_FILENAME, EntryPoint = "ReadEncoderResolution", CharSet = CharSet.Ansi)]
		private static extern bool _ReadEncoderResolution(ushort nNodeID, ref int nEncoderResolution);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteEncoderResolution", CharSet = CharSet.Ansi)]
		private static extern bool _WriteEncoderResolution(ushort nNodeID, int nEncoderResolution);

		// ES
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSingleEndedEncoderUsage", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSingleEndedEncoderUsage(ushort nNodeID, ref byte nSingleEndedEncoderUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSingleEndedEncoderUsage", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSingleEndedEncoderUsage(ushort nNodeID, byte nSingleEndedEncoderUsage);

		// EU
		[DllImport(DLL_FILENAME, EntryPoint = "ReadElectronicGearingRatioDenominator", CharSet = CharSet.Ansi)]
		private static extern bool _ReadElectronicGearingRatioDenominator(ushort nNodeID, ref int nElectronicGearingRatioDenominator);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteElectronicGearingRatioDenominator", CharSet = CharSet.Ansi)]
		private static extern bool _WriteElectronicGearingRatioDenominator(ushort nNodeID, int nElectronicGearingRatioDenominator);

		// FA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadFunctionofAnalogInput", CharSet = CharSet.Ansi)]
		private static extern bool _ReadFunctionofAnalogInput(ushort nNodeID, byte nAnalogChannel, ref byte nFunction);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteFunctionofAnalogInput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFunctionofAnalogInput(ushort nNodeID, byte nAnalogChannel, byte nFunction);

		// FC
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoLengthwithSpeedChange", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoLengthwithSpeedChange(ushort nNodeID, IntPtr nInputSensor, IntPtr chInputStatus, bool bWithOptionalX);

		// FD
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoDoubleSensor", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoDoubleSensor(ushort nNodeID, byte input1, char inputCondition1, byte input2, char inputCondition2);

		// FE
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFollowEncoder", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFollowEncoder(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// FH - Write
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFindHome", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFindHome(int nNodeID, int nHomingMethod);

		// FI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadFilterInput", CharSet = CharSet.Ansi)]
		private static extern bool _ReadFilterInput(ushort nNodeID, byte nInputSensor, ref int nFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteFilterInput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFilterInput(ushort nNodeID, byte nInputSensor, int nFilter);

		// FL
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoLength", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoLength(ushort nNodeID, IntPtr ptrDistance);

		// FM
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoSensorwithMaskDistance", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoSensorwithMaskDistance(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// FO
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoLengthandSetOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoLengthandSetOutput(ushort nNodeID, byte nOutput, char chOutputStatus, bool bWithOptionalY);

		// FP
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoPosition", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoPosition(ushort nNodeID, IntPtr ptrPosition);

		// FS
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoSensor", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoSensor(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// FX
		[DllImport(DLL_FILENAME, EntryPoint = "ReadFilterSelectInputs", CharSet = CharSet.Ansi)]
		private static extern bool _ReadFilterSelectInputs(ushort nNodeID, ref byte nFilterSelectInputs);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteFilterSelectInputs", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFilterSelectInputs(ushort nNodeID, byte nFilterSelectInputs);

		// FY
		[DllImport(DLL_FILENAME, EntryPoint = "WriteFeedtoSensorwithSafetyDistance", CharSet = CharSet.Ansi)]
		private static extern bool _WriteFeedtoSensorwithSafetyDistance(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// GG
		[DllImport(DLL_FILENAME, EntryPoint = "ReadGlobalGainSelection", CharSet = CharSet.Ansi)]
		private static extern bool _ReadGlobalGainSelection(ushort nNodeID, ref int nGlobalGainSelection);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteGlobalGainSelection", CharSet = CharSet.Ansi)]
		private static extern bool _WriteGlobalGainSelection(ushort nNodeID, int nGlobalGainSelection);

		// HA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingAcceleration", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHomingAcceleration(ushort nNodeID, int nStep, ref double dAccel);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingAcceleration", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHomingAcceleration(ushort nNodeID, int nStep, double dAccel);

		// HC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHardStopCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHardStopCurrent(ushort nNodeID, ref double dHardStopCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHardStopCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHardStopCurrent(ushort nNodeID, double dHardStopCurrent);

		// HD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHardStopFaultDelay", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHardStopFaultDelay(ushort nNodeID, ref int nHardStopFaultDelay);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHardStopFaultDelay", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHardStopFaultDelay(ushort nNodeID, int nHardStopFaultDelay);

		// HL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingDeceleration", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHomingDeceleration(ushort nNodeID, int nStep, ref double dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingDeceleration", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHomingDeceleration(ushort nNodeID, int nStep, double dDecel);

		// HO
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingOffset", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHomingOffset(ushort nNodeID, ref int nHomingOffset);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingOffset", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHomingOffset(ushort nNodeID, int nHomingOffset);

		// HS
		[DllImport(DLL_FILENAME, EntryPoint = "WriteHardStopHoming", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHardStopHoming(ushort nNodeID, bool bWithIndex);

		// HV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHomingVelocity(ushort nNodeID, int nStep, ref double dVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHomingVelocity(ushort nNodeID, int nStep, double dVelocity);

		// GC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadCurrentCommand", CharSet = CharSet.Ansi)]
		private static extern bool _ReadCurrentCommand(ushort nNodeID, ref int nCurrentCommand);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteCurrentCommand", CharSet = CharSet.Ansi)]
		private static extern bool _WriteCurrentCommand(ushort nNodeID, int nCurrentCommand);

		// HG
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHarmonicFilterGain", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHarmonicFilterGain(ushort nNodeID, ref int nHarmonicFilterGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHarmonicFilterGain", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHarmonicFilterGain(ushort nNodeID, int nHarmonicFilterGain);

		// HP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHarmonicFilterPhase", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHarmonicFilterPhase(ushort nNodeID, ref int nHarmonicFilterPhase);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHarmonicFilterPhase", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHarmonicFilterPhase(ushort nNodeID, int nHarmonicFilterPhase);

		// HW
		[DllImport(DLL_FILENAME, EntryPoint = "WriteHandWheel", CharSet = CharSet.Ansi)]
		private static extern bool _WriteHandWheel(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// IA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateAnalog", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateAnalog(ushort nNodeID, ref double dAnalogValue);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateAnalogWithChannel", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateAnalogWithChannel(ushort nNodeID, int nChannel, ref double dAnalogValue);

		// ID
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateDistance", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateDistance(ushort nNodeID, ref int nImmediatelyDistance);

		// IE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateEncoder", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateEncoder(ushort nNodeID, ref int nImmediatelyEncoder);

		// IF
		[DllImport(DLL_FILENAME, EntryPoint = "ReadHexFormat", CharSet = CharSet.Ansi)]
		private static extern bool _ReadHexFormat(ushort nNodeID, ref bool bHexFormat);

		// IH
		[DllImport(DLL_FILENAME, EntryPoint = "WriteImmediateHighOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteImmediateHighOutput(ushort nNodeID, int nOutput, bool bWithOptionalY);

		// IL
		[DllImport(DLL_FILENAME, EntryPoint = "WriteImmediateLowOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteImmediateLowOutput(ushort nNodeID, int nOutput, bool bWithOptionalY);

		// IC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateCommandedCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateCommandedCurrent(ushort nNodeID, ref double dImmediateCurrentCommanded);

		// IO
		[DllImport(DLL_FILENAME, EntryPoint = "ReadOutputStatus", CharSet = CharSet.Ansi)]
		private static extern bool _ReadOutputStatus(ushort nNodeID, ref byte chOutputStatus, bool bWithOptionalY);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteOutputStatus", CharSet = CharSet.Ansi)]
		private static extern bool _WriteOutputStatus(ushort nNodeID, byte chOutputStatus, bool bWithOptionalY);

		// IP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediatePosition", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediatePosition(ushort nNodeID, ref int nImmediatelyPosition);

		// IQ
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateActualCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateActualCurrent(ushort nNodeID, ref double dImmediateActualCurrent);

		// IS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadInputStatus", CharSet = CharSet.Ansi)]
		private static extern bool _ReadInputStatus(ushort nNodeID, ref int nInputStatus, bool bWithOptionalY);

		// IT
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateTemperature", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateTemperature(ushort nNodeID, ref double dTemperature);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateTemperatureWithChannel", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateTemperatureWithChannel(ushort nNodeID, int nChannel, ref double dTemperature);

		// IU
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateVoltage", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateVoltage(ushort nNodeID, ref double dVoltage);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateVoltageWithChannel", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateVoltageWithChannel(ushort nNodeID, int nChannel, ref double dVoltage);

		// IV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateActualVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateActualVelocity(ushort nNodeID, ref double dActualVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediateTargetVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediateTargetVelocity(ushort nNodeID, ref double dTargetVelocity);

		// IX
		[DllImport(DLL_FILENAME, EntryPoint = "ReadImmediatePositionError", CharSet = CharSet.Ansi)]
		private static extern bool _ReadImmediatePositionError(ushort nNodeID, ref int nImmediatelyPositionError);

		// JA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadJogAcceleration", CharSet = CharSet.Ansi)]
		private static extern bool _ReadJogAcceleration(ushort nNodeID, ref double dJogAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteJogAcceleration", CharSet = CharSet.Ansi)]
		private static extern bool _WriteJogAcceleration(ushort nNodeID, double dJogAcceleration);

		// JC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityModeSecondSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocityModeSecondSpeed(ushort nNodeID, ref double dVelocityModeSecondSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityModeSecondSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocityModeSecondSpeed(ushort nNodeID, double dVelocityModeSecondSpeed);

		// JD
		[DllImport(DLL_FILENAME, EntryPoint = "WriteJogDisable", CharSet = CharSet.Ansi)]
		private static extern bool _WriteJogDisable(ushort nNodeID);

		// JE
		[DllImport(DLL_FILENAME, EntryPoint = "WriteJogEnable", CharSet = CharSet.Ansi)]
		private static extern bool _WriteJogEnable(ushort nNodeID);

		// JL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadJogDeceleration", CharSet = CharSet.Ansi)]
		private static extern bool _ReadJogDeceleration(ushort nNodeID, ref double dJogDeceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteJogDeceleration", CharSet = CharSet.Ansi)]
		private static extern bool _WriteJogDeceleration(ushort nNodeID, double dJogDeceleration);

		// JM
		[DllImport(DLL_FILENAME, EntryPoint = "ReadJogMode", CharSet = CharSet.Ansi)]
		private static extern bool _ReadJogMode(ushort nNodeID, ref byte nJogMode);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteJogMode", CharSet = CharSet.Ansi)]
		private static extern bool _WriteJogMode(ushort nNodeID, byte nJogMode);

		// JS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadJogSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _ReadJogSpeed(ushort nNodeID, ref double dJogSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteJogSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _WriteJogSpeed(ushort nNodeID, double dJogSpeed);

		// KC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadOverallServoFilter", CharSet = CharSet.Ansi)]
		private static extern bool _ReadOverallServoFilter(ushort nNodeID, ref int nOverallServoFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteOverallServoFilter", CharSet = CharSet.Ansi)]
		private static extern bool _WriteOverallServoFilter(ushort nNodeID, int nOverallServoFilter);

		// KD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadDifferentialConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadDifferentialConstant(ushort nNodeID, ref int nDifferentialConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDifferentialConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDifferentialConstant(ushort nNodeID, int nDifferentialConstant);

		// KE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadDifferentialFilter", CharSet = CharSet.Ansi)]
		private static extern bool _ReadDifferentialFilter(ushort nNodeID, ref int nDifferentialFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDifferentialFilter", CharSet = CharSet.Ansi)]
		private static extern bool _WriteDifferentialFilter(ushort nNodeID, int nDifferentialFilter);

		// KF - Read
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityFeedforwardConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocityFeedforwardConstant(ushort nNodeID, ref int nVelocityFeedforwardConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityFeedforwardConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocityFeedforwardConstant(ushort nNodeID, int nVelocityFeedforwardConstant);

		// KG
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSecondaryGlobalGain", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSecondaryGlobalGain(ushort nNodeID, ref int nSecondaryGlobalGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSecondaryGlobalGain", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSecondaryGlobalGain(ushort nNodeID, int nSecondaryGlobalGain);

		// KI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadIntegratorConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadIntegratorConstant(ushort nNodeID, ref int nIntegratorConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteIntegratorConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteIntegratorConstant(ushort nNodeID, int nIntegratorConstant);

		// KJ
		[DllImport(DLL_FILENAME, EntryPoint = "ReadJerkFilterFrequency", CharSet = CharSet.Ansi)]
		private static extern bool _ReadJerkFilterFrequency(ushort nNodeID, ref int nJerkFilterFrequency);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteJerkFilterFrequency", CharSet = CharSet.Ansi)]
		private static extern bool _WriteJerkFilterFrequency(ushort nNodeID, int nJerkFilterFrequency);

		// KK
		[DllImport(DLL_FILENAME, EntryPoint = "ReadInertiaFeedforwardConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadInertiaFeedforwardConstant(ushort nNodeID, ref int nInertiaFeedforwardConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteInertiaFeedforwardConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteInertiaFeedforwardConstant(ushort nNodeID, int nInertiaFeedforwardConstant);

		// KP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadProportionalConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadProportionalConstant(ushort nNodeID, ref int nProportionalConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteProportionalConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteProportionalConstant(ushort nNodeID, int nProportionalConstant);

		// KV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityFeedbackConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocityFeedbackConstant(ushort nNodeID, ref int nVelocityFeedbackConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityFeedbackConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocityFeedbackConstant(ushort nNodeID, int nVelocityFeedbackConstant);

		// LA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadLeadAngleMaxValue", CharSet = CharSet.Ansi)]
		private static extern bool _ReadLeadAngleMaxValue(ushort nNodeID, ref byte nLeadAngleMaxValue);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteLeadAngleMaxValue", CharSet = CharSet.Ansi)]
		private static extern bool _WriteLeadAngleMaxValue(ushort nNodeID, byte nLeadAngleMaxValue);

		// LM
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSoftwareLimitCCW", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSoftwareLimitCCW(ushort nNodeID, ref int nSoftwareLimitCCW);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSoftwareLimitCCW", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSoftwareLimitCCW(ushort nNodeID, int nSoftwareLimitCCW);

		// LP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSoftwareLimitCW", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSoftwareLimitCW(ushort nNodeID, ref int nSoftwareLimitCW);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSoftwareLimitCW", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSoftwareLimitCW(ushort nNodeID, int nSoftwareLimitCW);

		// LS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadLeadAngleSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _ReadLeadAngleSpeed(ushort nNodeID, ref double dLeadAngleSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteLeadAngleSpeed", CharSet = CharSet.Ansi)]
		private static extern bool _WriteLeadAngleSpeed(ushort nNodeID, double dLeadAngleSpeed);

		// LV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadLowVoltageThreshold", CharSet = CharSet.Ansi)]
		private static extern bool _ReadLowVoltageThreshold(ushort nNodeID, ref int nLowVoltageThreshold);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteLowVoltageThreshold", CharSet = CharSet.Ansi)]
		private static extern bool _WriteLowVoltageThreshold(ushort nNodeID, int nLowVoltageThreshold);

		// MD
		[DllImport(DLL_FILENAME, EntryPoint = "WriteMotorDisable", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMotorDisable(ushort nNodeID);

		// ME
		[DllImport(DLL_FILENAME, EntryPoint = "WriteMotorEnable", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMotorEnable(ushort nNodeID);

		// MO
		[DllImport(DLL_FILENAME, EntryPoint = "ReadMotionOutput", CharSet = CharSet.Ansi)]
		private static extern bool _ReadMotionOutput(ushort nNodeID, ref byte nOutputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMotionOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMotionOutput(ushort nNodeID, byte nOutputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadMotionOutputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _ReadMotionOutputFlexIO(ushort nNodeID, ref byte nOutputUsage, ref byte nOutput);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMotionOutputFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMotionOutputFlexIO(ushort nNodeID, byte nOutputUsage, byte nOutput);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadMotionOutputMSeries", CharSet = CharSet.Ansi)]
		private static extern bool _ReadMotionOutputMSeries(ushort nNodeID, byte nOutput, ref byte nOutputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMotionOutputMSeries", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMotionOutputMSeries(ushort nNodeID, byte nOutput, byte nOutputUsage);

		// MR
		[DllImport(DLL_FILENAME, EntryPoint = "ReadMicrostepResolution", CharSet = CharSet.Ansi)]
		private static extern bool _ReadMicrostepResolution(ushort nNodeID, ref byte nMicrostepResolution);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMicrostepResolution", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMicrostepResolution(ushort nNodeID, byte nMicrostepResolution);

		// MS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadControlModeSelection", CharSet = CharSet.Ansi)]
		private static extern bool _ReadControlModeSelection(ushort nNodeID, ref byte nControlModeSelection);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteControlModeSelection", CharSet = CharSet.Ansi)]
		private static extern bool _WriteControlModeSelection(ushort nNodeID, byte nControlModeSelection);

		// MT
		[DllImport(DLL_FILENAME, EntryPoint = "ReadMultiTasking", CharSet = CharSet.Ansi)]
		private static extern bool _ReadMultiTasking(ushort nNodeID, ref byte nMultiTasking);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMultiTasking", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMultiTasking(ushort nNodeID, byte nMultiTasking);

		// MV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadModelRevision", CharSet = CharSet.Ansi)]
		private static extern bool _ReadModelRevision(ushort nNodeID, ref IntPtr ptrModelRevision);

		// OF
		[DllImport(DLL_FILENAME, EntryPoint = "WriteOnFault", CharSet = CharSet.Ansi)]
		private static extern bool _WriteOnFault(ushort nNodeID, byte nQSegment);

		// OI
		[DllImport(DLL_FILENAME, EntryPoint = "WriteOnInput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteOnInput(ushort nNodeID, IntPtr nInputSensor, IntPtr chInputStatus, bool bWithOptionalX);

		// OP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadOptionBoard", CharSet = CharSet.Ansi)]
		private static extern bool _ReadOptionBoard(ushort nNodeID, ref byte nOptionBoard);

		// PA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPowerupAccelerationCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPowerupAccelerationCurrent(ushort nNodeID, ref double dPowerupAccelerationCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePowerupAccelerationCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WritePowerupAccelerationCurrent(ushort nNodeID, double dPowerupAccelerationCurrent);

		// PB
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPowerupBaudRate", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPowerupBaudRate(ushort nNodeID, ref byte nPowerupBaudRate);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePowerupBaudRate", CharSet = CharSet.Ansi)]
		private static extern bool _WritePowerupBaudRate(ushort nNodeID, byte nPowerupBaudRate);

		// PC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPowerupCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPowerupCurrent(ushort nNodeID, ref double dPowerupCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePowerupCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WritePowerupCurrent(ushort nNodeID, double dPowerupCurrent);

		// PD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadInPositionCounts", CharSet = CharSet.Ansi)]
		private static extern bool _ReadInPositionCounts(ushort nNodeID, ref int nInPositionCounts);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteInPositionCounts", CharSet = CharSet.Ansi)]
		private static extern bool _WriteInPositionCounts(ushort nNodeID, int nInPositionCounts);

		// PE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadInPositionTiming", CharSet = CharSet.Ansi)]
		private static extern bool _ReadInPositionTiming(ushort nNodeID, ref int nInPositionTiming);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteInPositionTiming", CharSet = CharSet.Ansi)]
		private static extern bool _WriteInPositionTiming(ushort nNodeID, int nInPositionTiming);

		// PF
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPositionFault", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPositionFault(ushort nNodeID, ref int nPositionFault);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePositionFault", CharSet = CharSet.Ansi)]
		private static extern bool _WritePositionFault(ushort nNodeID, int nPositionFault);

		// PH
		[DllImport(DLL_FILENAME, EntryPoint = "ReadInhibitionOfPulseCommand", CharSet = CharSet.Ansi)]
		private static extern bool _ReadInhibitionOfPulseCommand(ushort nNodeID, ref int nInhibitionOfPulseCommand);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteInhibitionOfPulseCommand", CharSet = CharSet.Ansi)]
		private static extern bool _WriteInhibitionOfPulseCommand(ushort nNodeID, int nInhibitionOfPulseCommand);

		// PI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPowerupIdleCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPowerupIdleCurrent(ushort nNodeID, ref double dPowerupIdleCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePowerupIdleCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WritePowerupIdleCurrent(ushort nNodeID, double dPowerupIdleCurrent);

		// PK
		[DllImport(DLL_FILENAME, EntryPoint = "ReadParameterLock", CharSet = CharSet.Ansi)]
		private static extern bool _ReadParameterLock(ushort nNodeID, ref int nParameterLock);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteParameterLock", CharSet = CharSet.Ansi)]
		private static extern bool _WriteParameterLock(ushort nNodeID, int nParameterLock);

		// PL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPositionLimit", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPositionLimit(ushort nNodeID, ref int nPositionLimit);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePositionLimit", CharSet = CharSet.Ansi)]
		private static extern bool _WritePositionLimit(ushort nNodeID, int nPositionLimit);

		// PM
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPowerupMode", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPowerupMode(ushort nNodeID, ref byte nPowerupMode);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePowerupMode", CharSet = CharSet.Ansi)]
		private static extern bool _WritePowerupMode(ushort nNodeID, byte nPowerupMode);

		// PN
		[DllImport(DLL_FILENAME, EntryPoint = "WriteProbeonDemand", CharSet = CharSet.Ansi)]
		private static extern bool _WriteProbeonDemand(ushort nNodeID);

		// PP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPowerupPeakCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPowerupPeakCurrent(ushort nNodeID, ref double dPowerupPeakCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePowerupPeakCurrent", CharSet = CharSet.Ansi)]
		private static extern bool _WritePowerupPeakCurrent(ushort nNodeID, double dPowerupPeakCurrent);

		// PR
		[DllImport(DLL_FILENAME, EntryPoint = "ReadProtocol", CharSet = CharSet.Ansi)]
		private static extern bool _ReadProtocol(ushort nNodeID, ref byte nProtocol);

		// PT
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPulseType", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPulseType(ushort nNodeID, ref byte nPulseType);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePulseType", CharSet = CharSet.Ansi)]
		private static extern bool _WritePulseType(ushort nNodeID, byte nPulseType);

		// PV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSecondaryElectronicGearing", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSecondaryElectronicGearing(ushort nNodeID, ref byte nSecondaryElectronicGearing);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSecondaryElectronicGearing", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSecondaryElectronicGearing(ushort nNodeID, byte nSecondaryElectronicGearing);

		// QE
		[DllImport(DLL_FILENAME, EntryPoint = "WriteQueueExecute", CharSet = CharSet.Ansi)]
		private static extern bool _WriteQueueExecute(ushort nNodeID);

		// QK
		[DllImport(DLL_FILENAME, EntryPoint = "WriteQueueKill", CharSet = CharSet.Ansi)]
		private static extern bool _WriteQueueKill(ushort nNodeID);

		// QX
		[DllImport(DLL_FILENAME, EntryPoint = "WriteQueueLoadAndExecute", CharSet = CharSet.Ansi)]
		private static extern bool _WriteQueueLoadAndExecute(ushort nNodeID, byte nQSegment);

		// RC
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterCounter", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterCounter(ushort nNodeID, IntPtr nInputSensor, IntPtr chInputStatus, bool bWithOptionalX);

		// RD
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterDecrement", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterDecrement(ushort nNodeID, char chDataRegister);

		// RE
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRestart", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRestart(ushort nNodeID);

		// RI
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterIncrement", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterIncrement(ushort nNodeID, char chDataRegister);

		// RL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadRegisterLoad", CharSet = CharSet.Ansi)]
		private static extern bool _ReadRegisterLoad(ushort nNodeID, char chDataRegister, ref int nRegisterValue, bool bImmediately);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterLoad", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterLoad(ushort nNodeID, char chDataRegister, int nRegisterValue, bool bImmediately);

		// RM
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterMove", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterMove(ushort nNodeID, char chRegister1, char chRegister2);

		// RO
		[DllImport(DLL_FILENAME, EntryPoint = "ReadAntiResonanceOn", CharSet = CharSet.Ansi)]
		private static extern bool _ReadAntiResonanceOn(ushort nNodeID, ref bool bAntiResonanceOn);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAntiResonanceOn", CharSet = CharSet.Ansi)]
		private static extern bool _WriteAntiResonanceOn(ushort nNodeID, bool bAntiResonanceOn);

		// RR
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterRead", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterRead(ushort nNodeID, char chRegister, byte nLocation);

		// RS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadRequestStatus", CharSet = CharSet.Ansi)]
		private static extern bool _ReadRequestStatus(ushort nNodeID, ref IntPtr ptrRequestStatus);

		// RV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadRevisionLevel", CharSet = CharSet.Ansi)]
		private static extern bool _ReadRevisionLevel(ushort nNodeID, ref byte nRevisionLevel);

		// RW
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterWrite", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterWrite(ushort nNodeID, char chRegister1, byte chRegister2);

		// R+
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterAdd", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterAdd(ushort nNodeID, char chRegister1, char chRegister2);

		// R-
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterSubstract", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterSubstract(ushort nNodeID, char chRegister1, char chRegister2);

		// R*
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterMultiply", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterMultiply(ushort nNodeID, char chRegister1, char chRegister2);

		// R/
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterDivide", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterDivide(ushort nNodeID, char chRegister1, char chRegister2);

		// R&
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterAnd", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterAnd(ushort nNodeID, char chRegister1, char chRegister2);

		// R|
		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegisterOr", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegisterOr(ushort nNodeID, char chRegister1, char chRegister2);

		// SA
		[DllImport(DLL_FILENAME, EntryPoint = "WriteSaveParameters", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSaveParameters(ushort nNodeID);

		// SC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadStatusCode", CharSet = CharSet.Ansi)]
		private static extern bool _ReadStatusCode(ushort nNodeID, ref int nStatusCode);

		// SD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSetDirection", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSetDirection(ushort nNodeID, ref byte nDirection);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSetDirection", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSetDirection(ushort nNodeID, byte nInputSensor, char chDirection);

		// SF
		[DllImport(DLL_FILENAME, EntryPoint = "ReadStepFilterFrequency", CharSet = CharSet.Ansi)]
		private static extern bool _ReadStepFilterFrequency(ushort nNodeID, ref int nFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteStepFilterFrequency", CharSet = CharSet.Ansi)]
		private static extern bool _WriteStepFilterFrequency(ushort nNodeID, int nFilter);

		// SH
		[DllImport(DLL_FILENAME, EntryPoint = "WriteSeekHome", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSeekHome(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// SI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadEnableInputUsage", CharSet = CharSet.Ansi)]
		private static extern bool _ReadEnableInputUsage(ushort nNodeID, ref byte nInputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteEnableInputUsage", CharSet = CharSet.Ansi)]
		private static extern bool _WriteEnableInputUsage(ushort nNodeID, byte nInputUsage);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadEnableInputUsageFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _ReadEnableInputUsageFlexIO(ushort nNodeID, ref byte nInputUsage, ref byte nInputSensor);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteEnableInputUsageFlexIO", CharSet = CharSet.Ansi)]
		private static extern bool _WriteEnableInputUsageFlexIO(ushort nNodeID, byte nInputUsage, byte nInputSensor);

		// SJ
		[DllImport(DLL_FILENAME, EntryPoint = "WriteStopJogging", CharSet = CharSet.Ansi)]
		private static extern bool _WriteStopJogging(ushort nNodeID);

		// SK
		[DllImport(DLL_FILENAME, EntryPoint = "WriteStopAndKill", CharSet = CharSet.Ansi)]
		private static extern bool _WriteStopAndKill(ushort nNodeID, bool bWithOptionalD);

		// SM
		[DllImport(DLL_FILENAME, EntryPoint = "WriteStopMove", CharSet = CharSet.Ansi)]
		private static extern bool _WriteStopMove(ushort nNodeID, char chStopMode);

		// SO
		[DllImport(DLL_FILENAME, EntryPoint = "WriteSetOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSetOutput(ushort nNodeID, int nOutput, char chOutputStatus, bool bWithOptionalY);

		// SP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadSetPosition", CharSet = CharSet.Ansi)]
		private static extern bool _ReadSetPosition(ushort nNodeID, ref int nSetPosition);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSetPosition", CharSet = CharSet.Ansi)]
		private static extern bool _WriteSetPosition(ushort nNodeID, int nSetPosition);

		// ST
		[DllImport(DLL_FILENAME, EntryPoint = "WriteStop", CharSet = CharSet.Ansi)]
		private static extern bool _WriteStop(ushort nNodeID, bool bWithOptionalD);

		// TD
		[DllImport(DLL_FILENAME, EntryPoint = "ReadTransmitDelay", CharSet = CharSet.Ansi)]
		private static extern bool _ReadTransmitDelay(ushort nNodeID, ref int nTransmitDelay);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTransmitDelay", CharSet = CharSet.Ansi)]
		private static extern bool _WriteTransmitDelay(ushort nNodeID, int nTransmitDelay);

		// TI
		[DllImport(DLL_FILENAME, EntryPoint = "WriteTestInput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteTestInput(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// TO
		[DllImport(DLL_FILENAME, EntryPoint = "ReadTachOutput", CharSet = CharSet.Ansi)]
		private static extern bool _ReadTachOutput(ushort nNodeID, ref int nTachOutput);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTachOutput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteTachOutput(ushort nNodeID, int nTachOutput);

		// TR
		[DllImport(DLL_FILENAME, EntryPoint = "WriteTestRegister", CharSet = CharSet.Ansi)]
		private static extern bool _WriteTestRegister(ushort nNodeID, char chDataRegister, int nRegisterValue);

		// TS
		[DllImport(DLL_FILENAME, EntryPoint = "WriteTimeStamp", CharSet = CharSet.Ansi)]
		private static extern bool _WriteTimeStamp(ushort nNodeID);

		// TT
		[DllImport(DLL_FILENAME, EntryPoint = "ReadPulseCompleteTiming", CharSet = CharSet.Ansi)]
		private static extern bool _ReadPulseCompleteTiming(ushort nNodeID, ref int nPulseCompleteTiming);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePulseCompleteTiming", CharSet = CharSet.Ansi)]
		private static extern bool _WritePulseCompleteTiming(ushort nNodeID, int nPulseCompleteTiming);

		// TV
		[DllImport(DLL_FILENAME, EntryPoint = "ReadTorqueRipple", CharSet = CharSet.Ansi)]
		private static extern bool _ReadTorqueRipple(ushort nNodeID, ref double dTorqueRipple);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTorqueRipple", CharSet = CharSet.Ansi)]
		private static extern bool _WriteTorqueRipple(ushort nNodeID, double dTorqueRipple);

		// VC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityChange", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocityChange(ushort nNodeID, ref double dVelocityChange);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityChange", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocityChange(ushort nNodeID, double dVelocityChange);

		// VE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocity(ushort nNodeID, ref double dVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocity(ushort nNodeID, double dVelocity);

		// VI
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityIntegratorConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocityIntegratorConstant(ushort nNodeID, ref int nVelocityIntegratorConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityIntegratorConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocityIntegratorConstant(ushort nNodeID, int nVelocityIntegratorConstant);

		// VL
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVoltageLimit", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVoltageLimit(ushort nNodeID, ref int nVoltageLimit);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVoltageLimit", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVoltageLimit(ushort nNodeID, int nVoltageLimit);

		// VM
		[DllImport(DLL_FILENAME, EntryPoint = "ReadMaximumVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _ReadMaximumVelocity(ushort nNodeID, ref double dMaximumVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMaximumVelocity", CharSet = CharSet.Ansi)]
		private static extern bool _WriteMaximumVelocity(ushort nNodeID, double dMaximumVelocity);

		// VP
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityProportionalConstant", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocityProportionalConstant(ushort nNodeID, ref int nVelocityProportionalConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityProportionalConstant", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocityProportionalConstant(ushort nNodeID, int nVelocityProportionalConstant);

		// VR
		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityRipple", CharSet = CharSet.Ansi)]
		private static extern bool _ReadVelocityRipple(ushort nNodeID, ref double dVelocityRipple);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityRipple", CharSet = CharSet.Ansi)]
		private static extern bool _WriteVelocityRipple(ushort nNodeID, double dVelocityRipple);

		// WD
		[DllImport(DLL_FILENAME, EntryPoint = "WriteWaitDelay", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWaitDelay(ushort nNodeID, char chDataRegister);

		// WI
		[DllImport(DLL_FILENAME, EntryPoint = "WriteWaitforInput", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWaitforInput(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX);

		// WM
		[DllImport(DLL_FILENAME, EntryPoint = "WriteWaitonMove", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWaitonMove(ushort nNodeID);

		// WP
		[DllImport(DLL_FILENAME, EntryPoint = "WriteWaitPosition", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWaitPosition(ushort nNodeID);

		// WT
		[DllImport(DLL_FILENAME, EntryPoint = "WriteWaitTime", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWaitTime(ushort nNodeID, double dWaitTime);

		// ZC
		[DllImport(DLL_FILENAME, EntryPoint = "ReadRegenResistorWattage", CharSet = CharSet.Ansi)]
		private static extern bool _ReadRegenResistorWattage(ushort nNodeID, ref int nRegenResistorWattage);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegenResistorWattage", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegenResistorWattage(ushort nNodeID, int nRegenResistorWattage);

		// ZR
		[DllImport(DLL_FILENAME, EntryPoint = "ReadRegenResistorValue", CharSet = CharSet.Ansi)]
		private static extern bool _ReadRegenResistorValue(ushort nNodeID, ref int nRegenResistorValue);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegenResistorValue", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegenResistorValue(ushort nNodeID, int nRegenResistorValue);

		// ZT
		[DllImport(DLL_FILENAME, EntryPoint = "ReadRegenResistorPeakTime", CharSet = CharSet.Ansi)]
		private static extern bool _ReadRegenResistorPeakTime(ushort nNodeID, ref int nRegenResistorPeakTime);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteRegenResistorPeakTime", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRegenResistorPeakTime(ushort nNodeID, int nRegenResistorPeakTime);

		// ZE
		[DllImport(DLL_FILENAME, EntryPoint = "ReadWatchDogEnable", CharSet = CharSet.Ansi)]
		private static extern bool _ReadWatchDogEnable(ushort nNodeID, ref bool bEnable);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteWatchDogEnable", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWatchDogEnable(ushort nNodeID, bool bEnable);

		// ZS
		[DllImport(DLL_FILENAME, EntryPoint = "ReadWatchDogTimeOut", CharSet = CharSet.Ansi)]
		private static extern bool _ReadWatchDogTimeOut(ushort nNodeID, ref ushort bTimeOut);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteWatchDogTimeOut", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWatchDogTimeOut(ushort nNodeID, ushort bTimeOut);

		// ZA
		[DllImport(DLL_FILENAME, EntryPoint = "ReadWatchDogConfig", CharSet = CharSet.Ansi)]
		private static extern bool _ReadWatchDogConfig(ushort nNodeID, ref bool bFaultOutput, ref byte bConfig);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteWatchDogConfig", CharSet = CharSet.Ansi)]
		private static extern bool _WriteWatchDogConfig(ushort nNodeID, bool bFaultOutput, byte bConfig);

		#endregion

		#region Public Methods


		private static CSCallback m_OnDataSendCallback;

		private static CSCallback m_OnDataReceiveCallback;

		private static CSCallback m_OnCloseCallback;

		private static CSCallback m_OnConnectCallback;

		public delegate void OnDataSendOrReceiveEventHandler(EventArgs e);

		public event OnDataSendOrReceiveEventHandler OnDataSend;

		public event OnDataSendOrReceiveEventHandler OnDataReceive;

		public event OnDataSendOrReceiveEventHandler OnClose;

		public event OnDataSendOrReceiveEventHandler OnConnect;

		public void DataSendCallbackFunction()
		{
			if (OnDataSend != null)
			{
				EventArgs e = new EventArgs();
				OnDataSend(e);
			}
		}

		public void DataReceiveCallbackFunction()
		{
			if (OnDataReceive != null)
			{
				EventArgs e = new EventArgs();
				OnDataReceive(e);
			}
		}

		public void CloseCallbackFunction()
		{
			if (OnClose != null)
			{
				EventArgs e = new EventArgs();
				OnClose(e);
			}
		}

		public void ConnectCallbackFunction()
		{
			if (OnConnect != null)
			{
				EventArgs e = new EventArgs();
				OnConnect(e);
			}
		}

		public eSCLLibHelper()
		{
			m_OnDataSendCallback = DataSendCallbackFunction;
			_OnDataSend(m_OnDataSendCallback);

			m_OnDataReceiveCallback = DataReceiveCallbackFunction;
			_OnDataReceive(m_OnDataReceiveCallback);

			m_OnCloseCallback = CloseCallbackFunction;
			_OnClose(m_OnCloseCallback);


			m_OnConnectCallback = ConnectCallbackFunction;
			_OnConnect(m_OnConnectCallback);
		}

		~eSCLLibHelper()
		{

		}

		public bool Open(bool bTCPIP)
		{
			bool ret = _Open(bTCPIP);
			return ret;
		}

		public bool IsOpen()
		{
			return _IsOpen();
		}

		public bool IsOpenByNodeID(ushort nNodeID)
		{
			return _IsOpenByNodeID(nNodeID);
		}

		public bool Close()
		{
			bool ret = _Close();
			return ret;
		}

		public bool ClearInputBuffer(ushort nNodeID)
		{
			return _ClearInputBuffer(nNodeID);
		}

		public bool Ping(ushort nNodeID, ref int nBuildNo, ref string strMACID)
		{
			IntPtr strIntPtr = IntPtr.Zero;
			bool ret = _Ping(nNodeID, ref nBuildNo, ref strIntPtr);
			strMACID = Marshal.PtrToStringAnsi(strIntPtr);
			return ret;
		}

		public bool WakeUp(ushort nNodeID)
		{
			return _WakeUp(nNodeID);
		}

		public bool SetCommParam(bool bSave)
		{
			bool ret = _SetCommParam(bSave);
			return ret;
		}

		public void SetExecuteTimeOut(uint nExecuteTimeOut)
		{
			_SetExecuteTimeOut(nExecuteTimeOut);
		}

		public uint GetExecuteTimeOut()
		{
			return _GetExecuteTimeOut();
		}

		public void SetExecuteRetryTimes(byte nExecuteRetryTimes)
		{
			_SetExecuteRetryTimes(nExecuteRetryTimes);
		}

		public byte GetExecuteRetryTimes()
		{
			return _GetExecuteRetryTimes();
		}

		public bool SendSCLCommand(ushort nNodeID, byte[] command)
		{
			return _SendSCLCommand(nNodeID, command);
		}

		public bool SendSCLCommand(ushort nNodeID, string command)
		{
			byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(command);
			bool ret = SendSCLCommand(nNodeID, buf);
			return ret;
		}

		public bool ExecuteSCLCommand(ushort nNodeID, byte[] command, ref string strResponse)
		{
			IntPtr strIntPtr = IntPtr.Zero;
			bool ret = _ExecuteSCLCommand(nNodeID, command, ref strIntPtr);
			strResponse = Marshal.PtrToStringAnsi(strIntPtr);
			return ret;
		}

		public bool ExecuteSCLCommand(ushort nNodeID, string command, ref string strResponse)
		{
			byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(command);


			return ExecuteSCLCommand(nNodeID, buf, ref strResponse);
		}

		public void SetTriggerSendEvent(bool bTriggerSendEvent)
		{
			_SetTriggerSendEvent(bTriggerSendEvent);
		}

		public void SetTriggerReceiveEvent(bool bTriggerReceiveEvent)
		{
			_SetTriggerReceiveEvent(bTriggerReceiveEvent);
		}

		public void SetTCPAutoReconnect(bool bTCPAutoReconnect)
		{
			_SetTCPAutoReconnect(bTCPAutoReconnect);
		}

		public bool GetLastCommandSent(ushort nNodeID, ref CommandInfo commandInfo)
		{
			IntPtr ptrCommand = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CommandInfo)));
			bool ret = _GetLastCommandSent(nNodeID, ptrCommand);
			if (ret)
			{
				commandInfo = (CommandInfo)Marshal.PtrToStructure((IntPtr)((UInt32)ptrCommand), typeof(CommandInfo));
			}
			Marshal.FreeHGlobal(ptrCommand);
			return ret;
		}

		public bool GetLastCommandReceived(ushort nNodeID, ref CommandInfo commandInfo)
		{
			IntPtr ptrCommand = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CommandInfo)));
			bool ret = _GetLastCommandReceived(nNodeID, ptrCommand);
			if (ret)
			{
				commandInfo = (CommandInfo)Marshal.PtrToStructure((IntPtr)((UInt32)ptrCommand), typeof(CommandInfo));
			}
			Marshal.FreeHGlobal(ptrCommand);
			return ret;
		}

		public void GetLastErrorInfo(ref ErrorInfo errorInfo)
		{
			_GetLastErrorInfo(ref errorInfo);
		}

		// Add Axis
		public bool AddAxis(string strIPAddress)
		{
			return _AddAxis(strIPAddress);
		}

		// Clear All Axes
		public void ClearAllAxes()
		{
			_ClearAllAxes();
		}

		// Get Axes Count
		public ushort GetAxesCount()
		{
			return _GetAxesCount();
		}

		// Get IP Address List
		public void GetIPAddressListString(ref ushort nCount, ref string strIPAddressListString)
		{
			List<string> lst = new List<string>();
			IntPtr strIntPtr = IntPtr.Zero;
			_GetIPAddressListString(ref nCount, ref strIntPtr);

			strIPAddressListString = Marshal.PtrToStringAnsi(strIntPtr);
		}

		public bool DriveEnable(ushort nNodeID, bool bEnable)
		{
			return _DriveEnable(nNodeID, bEnable);
		}

		public bool SetDriveOutput(ushort nNodeID, byte nOutput1, char chOutputStatus1, byte? nOutput2 = null, char? chOutputStatus2 = null, byte? nOutput3 = null, char? chOutputStatus3 = null, byte? nOutput4 = null, char? chOutputStatus4 = null, byte? nOutput5 = null, char? chOutputStatus5 = null, byte? nOutput6 = null, char? chOutputStatus6 = null)
		{
			IntPtr ptrOutput2 = IntPtr.Zero;
			if (nOutput2.HasValue)
			{
				ptrOutput2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nOutput2.Value, ptrOutput2, true);
			}
			IntPtr ptrOutputStatus2 = IntPtr.Zero;
			if (chOutputStatus2.HasValue)
			{
				ptrOutputStatus2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chOutputStatus2.Value, ptrOutputStatus2, true);
			}
			IntPtr ptrOutput3 = IntPtr.Zero;
			if (nOutput3.HasValue)
			{
				ptrOutput3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nOutput3.Value, ptrOutput3, true);
			}
			IntPtr ptrOutputStatus3 = IntPtr.Zero;
			if (chOutputStatus3.HasValue)
			{
				ptrOutputStatus3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chOutputStatus3.Value, ptrOutputStatus3, true);
			}
			IntPtr ptrOutput4 = IntPtr.Zero;
			if (nOutput4.HasValue)
			{
				ptrOutput4 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nOutput4.Value, ptrOutput4, true);
			}
			IntPtr ptrOutputStatus4 = IntPtr.Zero;
			if (chOutputStatus4.HasValue)
			{
				ptrOutputStatus4 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chOutputStatus4.Value, ptrOutputStatus4, true);
			}
			IntPtr ptrOutput5 = IntPtr.Zero;
			if (nOutput5.HasValue)
			{
				ptrOutput5 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nOutput5.Value, ptrOutput5, true);
			}
			IntPtr ptrOutputStatus5 = IntPtr.Zero;
			if (chOutputStatus5.HasValue)
			{
				ptrOutputStatus5 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chOutputStatus5.Value, ptrOutputStatus5, true);
			}
			IntPtr ptrOutput6 = IntPtr.Zero;
			if (nOutput6.HasValue)
			{
				ptrOutput6 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nOutput6.Value, ptrOutput6, true);
			}
			IntPtr ptrOutputStatus6 = IntPtr.Zero;
			if (chOutputStatus6.HasValue)
			{
				ptrOutputStatus6 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chOutputStatus6.Value, ptrOutputStatus6, true);
			}
			bool ret = _SetDriveOutput(nNodeID, nOutput1, chOutputStatus1, ptrOutput2, ptrOutputStatus2, ptrOutput3, ptrOutputStatus3, ptrOutput4, ptrOutputStatus4, ptrOutput5, ptrOutputStatus5, ptrOutput6, ptrOutputStatus6);

			Marshal.FreeHGlobal(ptrOutput2);
			Marshal.FreeHGlobal(ptrOutput3);
			Marshal.FreeHGlobal(ptrOutput4);
			Marshal.FreeHGlobal(ptrOutput5);
			Marshal.FreeHGlobal(ptrOutput6);

			Marshal.FreeHGlobal(ptrOutputStatus2);
			Marshal.FreeHGlobal(ptrOutputStatus3);
			Marshal.FreeHGlobal(ptrOutputStatus4);
			Marshal.FreeHGlobal(ptrOutputStatus5);
			Marshal.FreeHGlobal(ptrOutputStatus6);

			return ret;
		}

		public bool SetP2PProfile(ushort nNodeID, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			bool ret = _SetP2PProfile(nNodeID, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);
			Marshal.FreeHGlobal(ptrVelocity);

			return ret;
		}

		public bool SetJogProfile(ushort nNodeID, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			bool ret = _SetJogProfile(nNodeID, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);
			Marshal.FreeHGlobal(ptrVelocity);

			return ret;
		}

		public bool IsMotorEnabled(ushort nNodeID)
		{
			return _IsMotorEnabled(nNodeID);
		}

		public bool IsSampling(ushort nNodeID)
		{
			return _IsSampling(nNodeID);
		}

		public bool IsInFault(ushort nNodeID)
		{
			return _IsInFault(nNodeID);
		}

		public bool IsInPosition(ushort nNodeID)
		{
			return _IsInPosition(nNodeID);
		}

		public bool IsMoving(ushort nNodeID)
		{
			return _IsMoving(nNodeID);
		}

		public bool IsJogging(ushort nNodeID)
		{
			return _IsJogging(nNodeID);
		}

		public bool IsStopping(ushort nNodeID)
		{
			return _IsStopping(nNodeID);
		}

		public bool IsWaitingforInput(ushort nNodeID)
		{
			return _IsWaitingforInput(nNodeID);
		}

		public bool IsSavingParam(ushort nNodeID)
		{
			return _IsSavingParam(nNodeID);
		}

		public bool IsInAlarm(ushort nNodeID)
		{
			return _IsInAlarm(nNodeID);
		}

		public bool IsHoming(ushort nNodeID)
		{
			return _IsHoming(nNodeID);
		}

		public bool IsWaitingforTime(ushort nNodeID)
		{
			return _IsWaitingforTime(nNodeID);
		}

		public bool IsRunningWizard(ushort nNodeID)
		{
			return _IsRunningWizard(nNodeID);
		}

		public bool IsCheckingEncoder(ushort nNodeID)
		{
			return _IsCheckingEncoder(nNodeID);
		}

		public bool IsRunningQProgram(ushort nNodeID)
		{
			return _IsRunningQProgram(nNodeID);
		}

		public bool IsInitializingOrServoReady(ushort nNodeID)
		{
			return _IsInitializingOrServoReady(nNodeID);
		}

		public bool IsInAlarmPositionLimit(ushort nNodeID)
		{
			return _IsInAlarmPositionLimit(nNodeID);
		}

		public bool IsInAlarmCWLimit(ushort nNodeID)
		{
			return _IsInAlarmCWLimit(nNodeID);
		}

		public bool IsInAlarmCCWLimit(ushort nNodeID)
		{
			return _IsInAlarmCCWLimit(nNodeID);
		}

		public bool IsInAlarmOverTemp(ushort nNodeID)
		{
			return _IsInAlarmOverTemp(nNodeID);
		}

		public bool IsInAlarmOverVoltage(ushort nNodeID)
		{
			return _IsInAlarmOverVoltage(nNodeID);
		}

		public bool IsInAlarmUnderVoltage(ushort nNodeID)
		{
			return _IsInAlarmUnderVoltage(nNodeID);
		}

		public bool IsInAlarmOverCurrent(ushort nNodeID)
		{
			return _IsInAlarmOverCurrent(nNodeID);
		}

		public bool IsInAlarmEncoderFault(ushort nNodeID)
		{
			return _IsInAlarmEncoderFault(nNodeID);
		}

		public bool IsInAlarmCommError(ushort nNodeID)
		{
			return _IsInAlarmCommError(nNodeID);
		}

		public bool IsInAlarmBadFlash(ushort nNodeID)
		{
			return _IsInAlarmBadFlash(nNodeID);
		}

		public bool IsInAlarmBlankQSegment(ushort nNodeID)
		{
			return _IsInAlarmBlankQSegment(nNodeID);
		}

		public bool IsInAlarmMoveWhileDisabledMSeries(ushort nNodeID)
		{
			return _IsInAlarmMoveWhileDisabledMSeries(nNodeID);
		}

		public bool IsInAlarmACPowerPhasseLostMSeries(ushort nNodeID)
		{
			return _IsInAlarmACPowerPhasseLostMSeries(nNodeID);
		}

		public bool IsInAlarmSafeTorqueOffMSeries(ushort nNodeID)
		{
			return _IsInAlarmSafeTorqueOffMSeries(nNodeID);
		}

		public bool IsInAlarmVelocityLimitMSeries(ushort nNodeID)
		{
			return _IsInAlarmVelocityLimitMSeries(nNodeID);
		}

		public bool IsInAlarmVoltageWarningMSeries(ushort nNodeID)
		{
			return _IsInAlarmVoltageWarningMSeries(nNodeID);
		}

		public bool RelMove(ushort nNodeID, int nDistance, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			bool ret = _RelMove(nNodeID, nDistance, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);
			Marshal.FreeHGlobal(ptrVelocity);

			return ret;
		}

		public bool AbsMove(ushort nNodeID, int nDistance, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			bool ret = _AbsMove(nNodeID, nDistance, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);
			Marshal.FreeHGlobal(ptrVelocity);

			return ret;
		}

		public bool FeedtoSensor(ushort nNodeID, int? nStopDistance, byte nInputSensor, char chInputStatus, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrStopDistance = IntPtr.Zero;
			if (nStopDistance != null)
			{
				ptrStopDistance = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nStopDistance.Value, ptrStopDistance, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			bool ret = _FeedtoSensor(nNodeID, ptrStopDistance, nInputSensor, chInputStatus, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrStopDistance);
			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);
			Marshal.FreeHGlobal(ptrVelocity);

			return ret;
		}

		public bool P2PMoveWithVelocityChange(ushort nNodeID, int? nDistance1, int? nDistance2, byte? nInputSensor, char? chInputStatus, double? dVelocity1, double? dVelocity2, double? dAccel, double? dDecel)
		{
			IntPtr ptrDistance1 = IntPtr.Zero;
			if (nDistance1.HasValue)
			{
				ptrDistance1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nDistance1.Value, ptrDistance1, true);
			}
			IntPtr ptrDistance2 = IntPtr.Zero;
			if (nDistance2.HasValue)
			{
				ptrDistance2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nDistance2.Value, ptrDistance2, true);
			}
			IntPtr ptrInputSensor = IntPtr.Zero;
			if (nInputSensor.HasValue)
			{
				ptrInputSensor = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nInputSensor.Value, ptrInputSensor, true);
			}
			IntPtr ptrInputStatus = IntPtr.Zero;
			if (chInputStatus.HasValue)
			{
				ptrInputStatus = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chInputStatus.Value, ptrInputStatus, true);
			}
			IntPtr ptrVelocity1 = IntPtr.Zero;
			if (dVelocity1 != null)
			{
				ptrVelocity1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity1.Value, ptrVelocity1, true);
			}
			IntPtr ptrVelocity2 = IntPtr.Zero;
			if (dVelocity2 != null)
			{
				ptrVelocity2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity2.Value, ptrVelocity2, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			bool ret = _P2PMoveWithVelocityChange(nNodeID, ptrDistance1, ptrDistance2, ptrInputSensor, ptrInputStatus, ptrVelocity1, ptrVelocity2, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrDistance1);
			Marshal.FreeHGlobal(ptrDistance2);
			Marshal.FreeHGlobal(ptrInputSensor);
			Marshal.FreeHGlobal(ptrInputStatus);
			Marshal.FreeHGlobal(ptrVelocity1);
			Marshal.FreeHGlobal(ptrVelocity2);
			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);

			return ret;
		}
		public bool P2PMoveAndSetOutput(ushort nNodeID, int? nMoveDistance, int? nSetOutputDistance, byte nOutput, char chOutputStatus, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrMoveDistance = IntPtr.Zero;
			if (nMoveDistance.HasValue)
			{
				ptrMoveDistance = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nMoveDistance.Value, ptrMoveDistance, true);
			}
			IntPtr ptrSetOutputDistance = IntPtr.Zero;
			if (nSetOutputDistance.HasValue)
			{
				ptrSetOutputDistance = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nSetOutputDistance.Value, ptrSetOutputDistance, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}

			bool ret = _P2PMoveAndSetOutput(nNodeID, ptrMoveDistance, ptrSetOutputDistance, nOutput, chOutputStatus, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrMoveDistance);
			Marshal.FreeHGlobal(ptrSetOutputDistance);
			Marshal.FreeHGlobal(ptrVelocity);
			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);

			return ret;
		}

		public bool FeedtoDoubleSensor(ushort nNodeID, byte nInputSensor1, char chCondition1, byte nInputSensor2, char chCondition2, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			bool ret = _FeedtoDoubleSensor(nNodeID, nInputSensor1, chCondition1, nInputSensor2, chCondition2, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);
			Marshal.FreeHGlobal(ptrVelocity);

			return ret;
		}

		public bool FeedtoSensorWithMaskDistance(ushort nNodeID, int? nStopDistance, int? nMaskDistance, byte nInputSensor, char chInputStatus, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrStopDistance = IntPtr.Zero;
			if (nStopDistance.HasValue)
			{
				ptrStopDistance = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nStopDistance.Value, ptrStopDistance, true);
			}
			IntPtr ptrMaskDistance = IntPtr.Zero;
			if (nMaskDistance.HasValue)
			{
				ptrMaskDistance = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nMaskDistance.Value, ptrMaskDistance, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			bool ret = _FeedtoSensorWithMaskDistance(nNodeID, ptrStopDistance, ptrMaskDistance, nInputSensor, chInputStatus, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrStopDistance);
			Marshal.FreeHGlobal(ptrMaskDistance);
			Marshal.FreeHGlobal(ptrVelocity);
			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);

			return ret;

		}

		public bool SeekHome(ushort nNodeID, byte nInputSensor, char chInputStatus, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			bool ret = _SeekHome(nNodeID, nInputSensor, chInputStatus, ptrVelocity, ptrAccel, ptrDecel);

			Marshal.FreeHGlobal(ptrAccel);
			Marshal.FreeHGlobal(ptrDecel);
			Marshal.FreeHGlobal(ptrVelocity);

			return ret;
		}

		public bool ExtendedSeekHome(ushort nNodeID, byte nInputSensor, char chInputStatus, double? dVelocity1, double? dVelocity2, double? dVelocity3, double? dAccel1, double? dAccel2, double? dAccel3, double? dDecel1, double? dDecel2, double? dDecel3)
		{
			IntPtr ptrVelocity1 = IntPtr.Zero;
			if (dVelocity1 != null)
			{
				ptrVelocity1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity1.Value, ptrVelocity1, true);
			}
			IntPtr ptrVelocity2 = IntPtr.Zero;
			if (dVelocity2 != null)
			{
				ptrVelocity2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity2.Value, ptrVelocity2, true);
			}
			IntPtr ptrVelocity3 = IntPtr.Zero;
			if (dVelocity3 != null)
			{
				ptrVelocity3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity3.Value, ptrVelocity3, true);
			}

			IntPtr ptrAccel1 = IntPtr.Zero;
			if (dAccel1 != null)
			{
				ptrAccel1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel1.Value, ptrAccel1, true);
			}
			IntPtr ptrAccel2 = IntPtr.Zero;
			if (dAccel2 != null)
			{
				ptrAccel2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel2.Value, ptrAccel2, true);
			}
			IntPtr ptrAccel3 = IntPtr.Zero;
			if (dAccel3 != null)
			{
				ptrAccel3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel3.Value, ptrAccel3, true);
			}

			IntPtr ptrDecel1 = IntPtr.Zero;
			if (dDecel1 != null)
			{
				ptrDecel1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel1.Value, ptrDecel1, true);
			}
			IntPtr ptrDecel2 = IntPtr.Zero;
			if (dDecel2 != null)
			{
				ptrDecel2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel2.Value, ptrDecel2, true);
			}
			IntPtr ptrDecel3 = IntPtr.Zero;
			if (dDecel3 != null)
			{
				ptrDecel3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel3.Value, ptrDecel3, true);
			}
			bool ret = _ExtendedSeekHome(nNodeID, nInputSensor, chInputStatus, ptrVelocity1, ptrVelocity2, ptrVelocity3, ptrAccel1, ptrAccel2, ptrAccel3, ptrDecel1, ptrDecel2, ptrDecel3);

			Marshal.FreeHGlobal(ptrVelocity1);
			Marshal.FreeHGlobal(ptrVelocity2);
			Marshal.FreeHGlobal(ptrVelocity3);
			Marshal.FreeHGlobal(ptrAccel1);
			Marshal.FreeHGlobal(ptrAccel2);
			Marshal.FreeHGlobal(ptrAccel3);
			Marshal.FreeHGlobal(ptrDecel1);
			Marshal.FreeHGlobal(ptrDecel2);
			Marshal.FreeHGlobal(ptrDecel3);

			return ret;

		}

		public bool HardStopHoming(int nNodeID, bool bWithIndex, double? dVelocity1, double? dVelocity2, double? dVelocity3, double? dAccel1, double? dAccel2, double? dAccel3, double? dDecel1, double? dDecel2, double? dDecel3)
		{
			IntPtr ptrVelocity1 = IntPtr.Zero;
			if (dVelocity1 != null)
			{
				ptrVelocity1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity1.Value, ptrVelocity1, true);
			}
			IntPtr ptrVelocity2 = IntPtr.Zero;
			if (dVelocity2 != null)
			{
				ptrVelocity2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity2.Value, ptrVelocity2, true);
			}
			IntPtr ptrVelocity3 = IntPtr.Zero;
			if (dVelocity3 != null)
			{
				ptrVelocity3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity3.Value, ptrVelocity3, true);
			}

			IntPtr ptrAccel1 = IntPtr.Zero;
			if (dAccel1 != null)
			{
				ptrAccel1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel1.Value, ptrAccel1, true);
			}
			IntPtr ptrAccel2 = IntPtr.Zero;
			if (dAccel2 != null)
			{
				ptrAccel2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel2.Value, ptrAccel2, true);
			}
			IntPtr ptrAccel3 = IntPtr.Zero;
			if (dAccel3 != null)
			{
				ptrAccel3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel3.Value, ptrAccel3, true);
			}

			IntPtr ptrDecel1 = IntPtr.Zero;
			if (dDecel1 != null)
			{
				ptrDecel1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel1.Value, ptrDecel1, true);
			}
			IntPtr ptrDecel2 = IntPtr.Zero;
			if (dDecel2 != null)
			{
				ptrDecel2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel2.Value, ptrDecel2, true);
			}
			IntPtr ptrDecel3 = IntPtr.Zero;
			if (dDecel3 != null)
			{
				ptrDecel3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel3.Value, ptrDecel3, true);
			}
			bool ret = _HardStopHoming(nNodeID, bWithIndex, ptrVelocity1, ptrVelocity2, ptrVelocity3, ptrAccel1, ptrAccel2, ptrAccel3, ptrDecel1, ptrDecel2, ptrDecel3);

			Marshal.FreeHGlobal(ptrVelocity1);
			Marshal.FreeHGlobal(ptrVelocity2);
			Marshal.FreeHGlobal(ptrVelocity3);
			Marshal.FreeHGlobal(ptrAccel1);
			Marshal.FreeHGlobal(ptrAccel2);
			Marshal.FreeHGlobal(ptrAccel3);
			Marshal.FreeHGlobal(ptrDecel1);
			Marshal.FreeHGlobal(ptrDecel2);
			Marshal.FreeHGlobal(ptrDecel3);

			return ret;
		}

		//AC
		public bool ReadAccelerationRate(ushort nNodeID, ref double dAccel)
		{
			return _ReadAccelerationRate(nNodeID, ref dAccel);
		}

		public bool WriteAccelerationRate(ushort nNodeID, double dAccel)
		{
			return _WriteAccelerationRate(nNodeID, dAccel);
		}

		// AD
		public bool ReadAnalogDeadband(ushort nNodeID, ref byte nAnalogDeadband)
		{
			return _ReadAnalogDeadband(nNodeID, ref nAnalogDeadband);
		}

		public bool WriteAnalogDeadband(ushort nNodeID, byte nAnalogDeadband)
		{
			return _WriteAnalogDeadband(nNodeID, nAnalogDeadband);
		}

		// AD
		public bool ReadAnalogDeadbandWithChannel(ushort nNodeID, byte nAnalogChannel, ref byte nAnalogDeadband)
		{
			return _ReadAnalogDeadbandWithChannel(nNodeID, nAnalogChannel, ref nAnalogDeadband);
		}

		public bool WriteAnalogDeadbandWithChannel(ushort nNodeID, byte nAnalogChannel, byte nAnalogDeadband)
		{
			return _WriteAnalogDeadbandWithChannel(nNodeID, nAnalogChannel, nAnalogDeadband);
		}

		// AF
		public bool ReadAnalogFilter(ushort nNodeID, ref int nAnalogFilter)
		{
			return _ReadAnalogFilter(nNodeID, ref nAnalogFilter);
		}

		public bool WriteAnalogFilter(ushort nNodeID, int nAnalogFilter)
		{
			return _WriteAnalogFilter(nNodeID, nAnalogFilter);
		}

		// AG
		public bool ReadAnalogVelocityGain(ushort nNodeID, ref int nAnalogVelocityGain)
		{
			return _ReadAnalogVelocityGain(nNodeID, ref nAnalogVelocityGain);
		}

		public bool WriteAnalogVelocityGain(ushort nNodeID, int nAnalogVelocityGain)
		{
			return _WriteAnalogVelocityGain(nNodeID, nAnalogVelocityGain);
		}

		// AI			   
		public bool ReadAlarmResetInput(ushort nNodeID, ref byte nAlarmResetInput)
		{
			return _ReadAlarmResetInput(nNodeID, ref nAlarmResetInput);
		}

		public bool WriteAlarmResetInput(ushort nNodeID, byte nAlarmResetInput)
		{
			return _WriteAlarmResetInput(nNodeID, nAlarmResetInput);
		}

		public bool ReadAlarmResetInputFlexIO(ushort nNodeID, ref byte nInputUsage, ref byte nInputSensor)
		{
			return _ReadAlarmResetInputFlexIO(nNodeID, ref nInputUsage, ref nInputSensor);
		}

		public bool WriteAlarmResetInputFlexIO(ushort nNodeID, byte nInputUsage, byte nInputSensor)
		{
			return _WriteAlarmResetInputFlexIO(nNodeID, nInputUsage, nInputSensor);
		}

		// AL
		public bool ReadAlarmCode(ushort nNodeID, ref int nAlarmCode)
		{
			return _ReadAlarmCode(nNodeID, ref nAlarmCode);
		}

		// AL
		public bool ReadAlarmCodeWithChannel(ushort nNodeID, byte nChannel, ref int nAlarmCode)
		{
			return _ReadAlarmCodeWithChannel(nNodeID, nChannel, ref nAlarmCode);
		}

		// AM
		public bool ReadMaxAcceleration(ushort nNodeID, ref double dMaxAcceleration)
		{
			return _ReadMaxAcceleration(nNodeID, ref dMaxAcceleration);
		}

		public bool WriteMaxAcceleration(ushort nNodeID, double dMaxAcceleration)
		{
			return _WriteMaxAcceleration(nNodeID, dMaxAcceleration);
		}

		// AN
		public bool ReadAnalogTorqueGain(ushort nNodeID, ref double nAnalogTorqueGain)
		{
			return _ReadAnalogTorqueGain(nNodeID, ref nAnalogTorqueGain);
		}

		public bool WriteAnalogTorqueGain(ushort nNodeID, double nAnalogTorqueGain)
		{
			return _WriteAnalogTorqueGain(nNodeID, nAnalogTorqueGain);
		}

		// AO
		public bool ReadAlarmOutput(ushort nNodeID, ref byte nOutputUsage)
		{
			return _ReadAlarmOutput(nNodeID, ref nOutputUsage);
		}

		public bool WriteAlarmOutput(ushort nNodeID, byte nOutputUsage)
		{
			return _WriteAlarmOutput(nNodeID, nOutputUsage);
		}

		public bool ReadAlarmOutputFlexIO(ushort nNodeID, ref byte nOutputUsage, ref byte nOutput)
		{
			return _ReadAlarmOutputFlexIO(nNodeID, ref nOutputUsage, ref nOutput);
		}

		public bool WriteAlarmOutputFlexIO(ushort nNodeID, byte nOutputUsage, byte nOutput)
		{
			return _WriteAlarmOutputFlexIO(nNodeID, nOutputUsage, nOutput);
		}

		// AP
		public bool ReadAnalogPositionGain(ushort nNodeID, ref int nPositionGain)
		{
			return _ReadAnalogPositionGain(nNodeID, ref nPositionGain);
		}

		public bool WriteAnalogPositionGain(ushort nNodeID, int nPositionGain)
		{
			return _WriteAnalogPositionGain(nNodeID, nPositionGain);
		}

		// AR
		public bool WriteAlarmReset(ushort nNodeID, bool bImmediately)
		{
			return _WriteAlarmReset(nNodeID, bImmediately);
		}

		// AS
		public bool ReadAnalogScaling(ushort nNodeID, ref byte scaling)
		{
			return _ReadAnalogScaling(nNodeID, ref scaling);
		}

		public bool WriteAnalogScaling(ushort nNodeID, byte scaling)
		{
			return _WriteAnalogScaling(nNodeID, scaling);
		}

		// AT
		public bool ReadAnalogThreshold(ushort nNodeID, ref double dAnalogThreshold)
		{
			return _ReadAnalogThreshold(nNodeID, ref dAnalogThreshold);
		}

		public bool WriteAnalogThreshold(ushort nNodeID, double dAnalogThreshold)
		{
			return _WriteAnalogThreshold(nNodeID, dAnalogThreshold);
		}

		// AV
		public bool ReadAnalogOffset(ushort nNodeID, ref double dAnalogOffset)
		{
			return _ReadAnalogOffset(nNodeID, ref dAnalogOffset);
		}

		public bool WriteAnalogOffset(ushort nNodeID, double dAnalogOffset)
		{
			return _WriteAnalogOffset(nNodeID, dAnalogOffset);
		}

		// AV
		public bool ReadAnalogOffsetMSeries(ushort nNodeID, byte nAnalogChannel, ref double dAnalogOffset)
		{
			return _ReadAnalogOffsetMSeries(nNodeID, nAnalogChannel, ref dAnalogOffset);
		}

		public bool WriteAnalogOffsetMSeries(ushort nNodeID, byte nAnalogChannel, double dAnalogOffset)
		{
			return _WriteAnalogOffsetMSeries(nNodeID, nAnalogChannel, dAnalogOffset);
		}

		// AZ
		public bool WriteAnalogZero(ushort nNodeID)
		{
			return _WriteAnalogZero(nNodeID);
		}

		// BO		
		public bool ReadBrakeOutput(ushort nNodeID, ref byte nOutputUsage)
		{
			return _ReadBrakeOutput(nNodeID, ref nOutputUsage);
		}

		public bool WriteBrakeOutput(ushort nNodeID, byte nOutputUsage)
		{
			return _WriteBrakeOutput(nNodeID, nOutputUsage);
		}

		public bool ReadBrakeOutputFlexIO(ushort nNodeID, ref byte nOutputUsage, ref byte nOutput)
		{
			return _ReadBrakeOutputFlexIO(nNodeID, ref nOutputUsage, ref nOutput);
		}

		public bool WriteBrakeOutputFlexIO(ushort nNodeID, byte nOutputUsage, byte nOutput)
		{
			return _WriteBrakeOutputFlexIO(nNodeID, nOutputUsage, nOutput);
		}

		// BD
		public bool ReadBrakeDisengageDelay(ushort nNodeID, ref double dBrakeDisengageDelay)
		{
			return _ReadBrakeDisengageDelay(nNodeID, ref dBrakeDisengageDelay);
		}

		public bool WriteBrakeDisengageDelay(ushort nNodeID, double dBrakeDisengageDelay)
		{
			return _WriteBrakeDisengageDelay(nNodeID, dBrakeDisengageDelay);
		}

		// BE
		public bool ReadBrakeEngageDelay(ushort nNodeID, ref double dBrakeEngageDelay)
		{
			return _ReadBrakeEngageDelay(nNodeID, ref dBrakeEngageDelay);
		}

		public bool WriteBrakeEngageDelay(ushort nNodeID, double dBrakeEngageDelay)
		{
			return _WriteBrakeEngageDelay(nNodeID, dBrakeEngageDelay);
		}

		// BS
		public bool ReadBufferStatus(ushort nNodeID, ref byte nBufferStatus)
		{
			return _ReadBufferStatus(nNodeID, ref nBufferStatus);
		}

		// CA
		public bool ReadChangeAccelerationCurrent(ushort nNodeID, ref double dChangeAccelerationCurrent)
		{
			return _ReadChangeAccelerationCurrent(nNodeID, ref dChangeAccelerationCurrent);
		}

		public bool WriteChangeAccelerationCurrent(ushort nNodeID, double dChangeAccelerationCurrent)
		{
			return _WriteChangeAccelerationCurrent(nNodeID, dChangeAccelerationCurrent);
		}

		// CC
		public bool ReadChangeCurrent(ushort nNodeID, ref double dChangeCurrent)
		{
			return _ReadChangeCurrent(nNodeID, ref dChangeCurrent);
		}

		public bool WriteChangeCurrent(ushort nNodeID, double dChangeCurrent)
		{
			return _WriteChangeCurrent(nNodeID, dChangeCurrent);
		}

		// CD
		public bool ReadIdleCurrentDelayTime(ushort nNodeID, ref double dIdleCurrentDelayTime)
		{
			return _ReadIdleCurrentDelayTime(nNodeID, ref dIdleCurrentDelayTime);
		}

		public bool WriteIdleCurrentDelayTime(ushort nNodeID, double dIdleCurrentDelayTime)
		{
			return _WriteIdleCurrentDelayTime(nNodeID, dIdleCurrentDelayTime);
		}

		// CE
		public bool ReadCommunicationError(ushort nNodeID, ref int nCommunicationError)
		{
			return _ReadCommunicationError(nNodeID, ref nCommunicationError);
		}

		// CF
		public bool ReadAntiResonanceFilterFreq(ushort nNodeID, ref int nAntiResonanceFilterFreq)
		{
			return _ReadAntiResonanceFilterFreq(nNodeID, ref nAntiResonanceFilterFreq);
		}

		public bool WriteAntiResonanceFilterFreq(ushort nNodeID, int nAntiResonanceFilterFreq)
		{
			return _WriteAntiResonanceFilterFreq(nNodeID, nAntiResonanceFilterFreq);
		}

		// CG
		public bool ReadAntiResonanceFilterGain(ushort nNodeID, ref int nAntiResonanceFilterGain)
		{
			return _ReadAntiResonanceFilterGain(nNodeID, ref nAntiResonanceFilterGain);
		}

		public bool WriteAntiResonanceFilterGain(ushort nNodeID, int nAntiResonanceFilterGain)
		{
			return _WriteAntiResonanceFilterGain(nNodeID, nAntiResonanceFilterGain);
		}

		// CI
		public bool ReadChangeIdleCurrent(ushort nNodeID, ref double dChangeIdleCurrent)
		{
			return _ReadChangeIdleCurrent(nNodeID, ref dChangeIdleCurrent);
		}

		public bool WriteChangeIdleCurrent(ushort nNodeID, double dChangeIdleCurrent)
		{
			return _WriteChangeIdleCurrent(nNodeID, dChangeIdleCurrent);
		}

		//CJ
		public bool WriteCommenceJogging(ushort nNodeID)
		{
			return _WriteCommenceJogging(nNodeID);
		}

		// CM
		public bool ReadCommandMode(ushort nNodeID, ref byte nCommandMode)
		{
			return _ReadCommandMode(nNodeID, ref nCommandMode);
		}

		public bool WriteCommandMode(ushort nNodeID, byte nCommandMode)
		{
			return _WriteCommandMode(nNodeID, nCommandMode);
		}

		// CN
		public bool ReadSecondaryCommandMode(ushort nNodeID, ref byte nSecondaryCommandMode)
		{
			return _ReadSecondaryCommandMode(nNodeID, ref nSecondaryCommandMode);
		}

		public bool WriteSecondaryCommandMode(ushort nNodeID, byte nSecondaryCommandMode)
		{
			return _WriteSecondaryCommandMode(nNodeID, nSecondaryCommandMode);
		}

		// CP
		public bool ReadChangePeakCurrent(ushort nNodeID, ref double dChangePeakCurrent)
		{
			return _ReadChangePeakCurrent(nNodeID, ref dChangePeakCurrent);
		}

		public bool WriteChangePeakCurrent(ushort nNodeID, double dChangePeakCurrent)
		{
			return _WriteChangePeakCurrent(nNodeID, dChangePeakCurrent);
		}

		// CR
		public bool WriteCompareRegisters(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteCompareRegisters(nNodeID, chRegister1, chRegister2);
		}

		// CS
		public bool ReadChangeSpeed(ushort nNodeID, ref double dChangeSpeed)
		{
			return _ReadChangeSpeed(nNodeID, ref dChangeSpeed);
		}

		public bool WriteChangeSpeed(ushort nNodeID, double dChangeSpeed)
		{
			return _WriteChangeSpeed(nNodeID, dChangeSpeed);
		}

		// DC
		public bool ReadChangeDistance(ushort nNodeID, ref int nChangeDistance)
		{
			return _ReadChangeDistance(nNodeID, ref nChangeDistance);
		}

		public bool WriteChangeDistance(ushort nNodeID, int nChangeDistance)
		{
			return _WriteChangeDistance(nNodeID, nChangeDistance);
		}

		// DD
		public bool ReadDefaultDisplay(ushort nNodeID, ref int nDefaultDisplay)
		{
			return _ReadDefaultDisplay(nNodeID, ref nDefaultDisplay);
		}

		public bool WriteDefaultDisplay(ushort nNodeID, int nDefaultDisplay)
		{
			return _WriteDefaultDisplay(nNodeID, nDefaultDisplay);
		}

		// DE
		public bool ReadDecelerationRate(ushort nNodeID, ref double dDecel)
		{
			return _ReadDecelerationRate(nNodeID, ref dDecel);
		}

		public bool WriteDecelerationRate(ushort nNodeID, double dDecel)
		{
			return _WriteDecelerationRate(nNodeID, dDecel);
		}

		// DI
		public bool ReadDistanceOrPosition(ushort nNodeID, ref int nDistance)
		{
			return _ReadDistanceOrPosition(nNodeID, ref nDistance);
		}

		public bool WriteDistanceOrPosition(ushort nNodeID, int nDistance)
		{
			return _WriteDistanceOrPosition(nNodeID, nDistance);
		}

		// DL
		public bool ReadDefineLimits(ushort nNodeID, ref byte nDefineLimits)
		{
			return _ReadDefineLimits(nNodeID, ref nDefineLimits);
		}

		public bool WriteDefineLimits(ushort nNodeID, byte nDefineLimits)
		{
			return _WriteDefineLimits(nNodeID, nDefineLimits);
		}

		// DP
		public bool ReadDumpingPower(ushort nNodeID, ref int nDumpingPower)
		{
			return _ReadDumpingPower(nNodeID, ref nDumpingPower);
		}

		public bool WriteDumpingPower(ushort nNodeID, int nDumpingPower)
		{
			return _WriteDumpingPower(nNodeID, nDumpingPower);
		}

		// DR
		public bool WriteDataRegisterforCapture(ushort nNodeID, char chDataRegisterforCapture)
		{
			return _WriteDataRegisterforCapture(nNodeID, chDataRegisterforCapture);
		}

		// DS
		public bool ReadSwitchingElectronicGearing(ushort nNodeID, ref byte nSwitchingElectronicGearing)
		{
			return _ReadSwitchingElectronicGearing(nNodeID, ref nSwitchingElectronicGearing);
		}

		// DS
		public bool WriteSwitchingElectronicGearing(ushort nNodeID, byte nSwitchingElectronicGearing)
		{
			return _WriteSwitchingElectronicGearing(nNodeID, nSwitchingElectronicGearing);
		}

		// ED
		public bool ReadEncoderDirection(ushort nNodeID, ref byte nEncoderDirection)
		{
			return _ReadEncoderDirection(nNodeID, ref nEncoderDirection);
		}

		public bool WriteEncoderDirection(ushort nNodeID, byte nEncoderDirection)
		{
			return _WriteEncoderDirection(nNodeID, nEncoderDirection);
		}

		// EF
		public bool ReadEncoderFunction(ushort nNodeID, ref byte nEncoderFunction)
		{
			return _ReadEncoderFunction(nNodeID, ref nEncoderFunction);
		}

		public bool WriteEncoderFunction(ushort nNodeID, byte nEncoderFunction)
		{
			return _WriteEncoderFunction(nNodeID, nEncoderFunction);
		}

		// EG
		public bool ReadElectronicGearing(ushort nNodeID, ref int nElectronicGearing)
		{
			return _ReadElectronicGearing(nNodeID, ref nElectronicGearing);
		}

		public bool WriteElectronicGearing(ushort nNodeID, int nElectronicGearing)
		{
			return _WriteElectronicGearing(nNodeID, nElectronicGearing);
		}

		// EH - Write
		public bool WriteExtendedHoming(int nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteExtendedHoming(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// EI
		public bool ReadInputNoiseFilter(ushort nNodeID, ref byte nInputNoiseFilter)
		{
			return _ReadInputNoiseFilter(nNodeID, ref nInputNoiseFilter);
		}

		public bool WriteInputNoiseFilter(ushort nNodeID, byte nInputNoiseFilter)
		{
			return _WriteInputNoiseFilter(nNodeID, nInputNoiseFilter);
		}

		// EN
		public bool ReadElectronicGearingRatioNumerator(ushort nNodeID, ref int nElectronicGearingRatioNumerator)
		{
			return _ReadElectronicGearingRatioNumerator(nNodeID, ref nElectronicGearingRatioNumerator);
		}

		public bool WriteElectronicGearingRatioNumerator(ushort nNodeID, int nElectronicGearingRatioNumerator)
		{
			return _WriteElectronicGearingRatioNumerator(nNodeID, nElectronicGearingRatioNumerator);
		}

		// EP
		public bool ReadEncoderPosition(ushort nNodeID, ref int nEncoderPosition)
		{
			return _ReadEncoderPosition(nNodeID, ref nEncoderPosition);
		}

		public bool WriteEncoderPosition(ushort nNodeID, int nEncoderPosition)
		{
			return _WriteEncoderPosition(nNodeID, nEncoderPosition);
		}

		// ER
		public bool ReadEncoderResolution(ushort nNodeID, ref int nEncoderResolution)
		{
			return _ReadEncoderResolution(nNodeID, ref nEncoderResolution);
		}

		public bool WriteEncoderResolution(ushort nNodeID, int nEncoderResolution)
		{
			return _WriteEncoderResolution(nNodeID, nEncoderResolution);
		}

		// ES
		public bool ReadSingleEndedEncoderUsage(ushort nNodeID, ref byte nSingleEndedEncoderUsage)
		{
			return _ReadSingleEndedEncoderUsage(nNodeID, ref nSingleEndedEncoderUsage);
		}

		public bool WriteSingleEndedEncoderUsage(ushort nNodeID, byte nSingleEndedEncoderUsage)
		{
			return _WriteSingleEndedEncoderUsage(nNodeID, nSingleEndedEncoderUsage);
		}

		// EU
		public bool ReadElectronicGearingRatioDenominator(ushort nNodeID, ref int nElectronicGearingRatioDenominator)
		{
			return _ReadElectronicGearingRatioDenominator(nNodeID, ref nElectronicGearingRatioDenominator);
		}

		public bool WriteElectronicGearingRatioDenominator(ushort nNodeID, int nElectronicGearingRatioDenominator)
		{
			return _WriteElectronicGearingRatioDenominator(nNodeID, nElectronicGearingRatioDenominator);
		}

		// FA
		public bool ReadFunctionofAnalogInput(ushort nNodeID, byte nAnalogChannel, ref byte nFunctionofAnalogInput)
		{
			return _ReadFunctionofAnalogInput(nNodeID, nAnalogChannel, ref nFunctionofAnalogInput);
		}

		public bool WriteFunctionofAnalogInput(ushort nNodeID, byte nAnalogChannel, byte nFunctionofAnalogInput)
		{
			return _WriteFunctionofAnalogInput(nNodeID, nAnalogChannel, nFunctionofAnalogInput);
		}

		// FC
		public bool WriteFeedtoLengthwithSpeedChange(ushort nNodeID, byte? nInputSensor, char? chInputStatus, bool bWithOptionalX = false)
		{
			IntPtr ptrInputSensor = IntPtr.Zero;
			if (nInputSensor.HasValue)
			{
				ptrInputSensor = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nInputSensor.Value, ptrInputSensor, true);
			}
			IntPtr ptrInputStatus = IntPtr.Zero;
			if (chInputStatus.HasValue)
			{
				ptrInputStatus = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chInputStatus.Value, ptrInputStatus, true);
			}
			bool ret = _WriteFeedtoLengthwithSpeedChange(nNodeID, ptrInputSensor, ptrInputStatus, bWithOptionalX);

			Marshal.FreeHGlobal(ptrInputSensor);
			Marshal.FreeHGlobal(ptrInputStatus);

			return ret;
		}

		// FD
		public bool WriteFeedtoDoubleSensor(ushort nNodeID, byte input1, char inputCondition1, byte input2, char inputCondition2)
		{
			return _WriteFeedtoDoubleSensor(nNodeID, input1, inputCondition1, input2, inputCondition2);
		}

		// FE
		public bool WriteFollowEncoder(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteFollowEncoder(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// FH - Write
		public bool WriteFindHome(int nNodeID, int nHomingMethod)
		{
			return _WriteFindHome(nNodeID, nHomingMethod);
		}

		// FI
		public bool ReadFilterInput(ushort nNodeID, byte nInputSensor, ref int nFilter)
		{
			return _ReadFilterInput(nNodeID, nInputSensor, ref nFilter);
		}

		public bool WriteFilterInput(ushort nNodeID, byte nInputSensor, int nFilter)
		{
			return _WriteFilterInput(nNodeID, nInputSensor, nFilter);
		}


		// FL
		public bool WriteFeedtoLength(ushort nNodeID, int? nDistance = null)
		{
			IntPtr ptrDistance = IntPtr.Zero;

			if (nDistance.HasValue)
			{
				ptrDistance = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nDistance.Value, ptrDistance, true);
			}

			bool ret = _WriteFeedtoLength(nNodeID, ptrDistance);

			Marshal.FreeHGlobal(ptrDistance);

			return ret;
		}

		// FM
		public bool WriteFeedtoSensorwithMaskDistance(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteFeedtoSensorwithMaskDistance(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// FO
		public bool WriteFeedtoLengthandSetOutput(ushort nNodeID, byte nOutput, char chOutputStatus, bool bWithOptionalY = false)
		{
			return _WriteFeedtoLengthandSetOutput(nNodeID, nOutput, chOutputStatus, bWithOptionalY);
		}

		// FP
		public bool WriteFeedtoPosition(ushort nNodeID, int? nPosition = null)
		{
			IntPtr ptrPosition = IntPtr.Zero;

			if (nPosition.HasValue)
			{
				ptrPosition = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(nPosition.Value, ptrPosition, true);
			}
			bool ret = _WriteFeedtoPosition(nNodeID, ptrPosition);

			Marshal.FreeHGlobal(ptrPosition);

			return ret;
		}

		// FS
		public bool WriteFeedtoSensor(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteFeedtoSensor(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// FX
		public bool ReadFilterSelectInputs(ushort nNodeID, ref byte nFilterSelectInputs)
		{
			return _ReadFilterSelectInputs(nNodeID, ref nFilterSelectInputs);
		}

		public bool WriteFilterSelectInputs(ushort nNodeID, byte nFilterSelectInputs)
		{
			return _WriteFilterSelectInputs(nNodeID, nFilterSelectInputs);
		}

		// FY
		public bool WriteFeedtoSensorwithSafetyDistance(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteFeedtoSensorwithSafetyDistance(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// GC
		public bool ReadCurrentCommand(ushort nNodeID, ref int nCurrentCommand)
		{
			return _ReadCurrentCommand(nNodeID, ref nCurrentCommand);
		}

		public bool WriteCurrentCommand(ushort nNodeID, int nCurrentCommand)
		{
			return _WriteCurrentCommand(nNodeID, nCurrentCommand);
		}

		// GG
		public bool ReadGlobalGainSelection(ushort nNodeID, ref int nGlobalBainSelection)
		{
			return _ReadGlobalGainSelection(nNodeID, ref nGlobalBainSelection);
		}

		public bool WriteGlobalGainSelection(ushort nNodeID, int nGlobalBainSelection)
		{
			return _WriteGlobalGainSelection(nNodeID, nGlobalBainSelection);
		}

		// HA
		public bool ReadHomingAcceleration(ushort nNodeID, int nStep, ref double dMaximumVelocity)
		{
			return _ReadHomingAcceleration(nNodeID, nStep, ref dMaximumVelocity);
		}

		public bool WriteHomingAcceleration(ushort nNodeID, int nStep, double dMaximumVelocity)
		{
			return _WriteHomingAcceleration(nNodeID, nStep, dMaximumVelocity);
		}

		// HC
		public bool ReadHardStopCurrent(ushort nNodeID, ref double nHardStopCurrent)
		{
			return _ReadHardStopCurrent(nNodeID, ref nHardStopCurrent);
		}

		public bool WriteHardStopCurrent(ushort nNodeID, double nHardStopCurrent)
		{
			return _WriteHardStopCurrent(nNodeID, nHardStopCurrent);
		}

		// HD
		public bool ReadHardStopFaultDelay(ushort nNodeID, ref int nHardStopFaultDelay)
		{
			return _ReadHardStopFaultDelay(nNodeID, ref nHardStopFaultDelay);
		}

		public bool WriteHardStopFaultDelay(ushort nNodeID, int nHardStopFaultDelay)
		{
			return _WriteHardStopFaultDelay(nNodeID, nHardStopFaultDelay);
		}

		// HG
		public bool ReadHarmonicFilterGain(ushort nNodeID, ref int nHarmonicFilterGain)
		{
			return _ReadHarmonicFilterGain(nNodeID, ref nHarmonicFilterGain);
		}

		public bool WriteHarmonicFilterGain(ushort nNodeID, int nHarmonicFilterGain)
		{
			return _WriteHarmonicFilterGain(nNodeID, nHarmonicFilterGain);
		}

		// HO
		public bool ReadHomingOffset(ushort nNodeID, ref int nDistance)
		{
			return _ReadHomingOffset(nNodeID, ref nDistance);
		}

		public bool WriteHomingOffset(ushort nNodeID, int nDistance)
		{
			return _WriteHomingOffset(nNodeID, nDistance);
		}

		// HL
		public bool ReadHomingDeceleration(ushort nNodeID, int nStep, ref double dMaximumVelocity)
		{
			return _ReadHomingDeceleration(nNodeID, nStep, ref dMaximumVelocity);
		}

		public bool WriteHomingDeceleration(ushort nNodeID, int nStep, double dMaximumVelocity)
		{
			return _WriteHomingDeceleration(nNodeID, nStep, dMaximumVelocity);
		}

		// HP
		public bool ReadHarmonicFilterPhase(ushort nNodeID, ref int nHarmonicFilterPhase)
		{
			return _ReadHarmonicFilterPhase(nNodeID, ref nHarmonicFilterPhase);
		}

		public bool WriteHarmonicFilterPhase(ushort nNodeID, int nHarmonicFilterPhase)
		{
			return _WriteHarmonicFilterPhase(nNodeID, nHarmonicFilterPhase);
		}

		// HS

		public bool WriteHardStopHoming(ushort nNodeID, bool bWithIndex)
		{
			return _WriteHardStopHoming(nNodeID, bWithIndex);
		}

		// HV
		public bool ReadHomingVelocity(ushort nNodeID, int nStep, ref double dMaximumVelocity)
		{
			return _ReadHomingVelocity(nNodeID, nStep, ref dMaximumVelocity);
		}

		public bool WriteHomingVelocity(ushort nNodeID, int nStep, double dMaximumVelocity)
		{
			return _WriteHomingVelocity(nNodeID, nStep, dMaximumVelocity);
		}

		// HW
		public bool WriteHandWheel(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteHandWheel(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// IA
		public bool ReadImmediateAnalog(ushort nNodeID, ref double nAnalogValue)
		{
			return _ReadImmediateAnalog(nNodeID, ref nAnalogValue);
		}


		public bool ReadImmediateAnalogWithChannel(ushort nNodeID, int nChannel, ref double dAnalogValue)
		{
			return _ReadImmediateAnalogWithChannel(nNodeID, nChannel, ref dAnalogValue);
		}

		// ID
		public bool ReadImmediateDistance(ushort nNodeID, ref int nImmediateDistance)
		{
			return _ReadImmediateDistance(nNodeID, ref nImmediateDistance);
		}

		// IE
		public bool ReadImmediateEncoder(ushort nNodeID, ref int nImmediatelyEncoder)
		{
			return _ReadImmediateEncoder(nNodeID, ref nImmediatelyEncoder);
		}

		// IF
		public bool ReadHexFormat(ushort nNodeID, ref bool bHexFormat)
		{
			return _ReadHexFormat(nNodeID, ref bHexFormat);
		}

		// IH
		public bool WriteImmediateHighOutput(ushort nNodeID, int nOutput, bool bWithOptionalY = false)
		{
			return _WriteImmediateHighOutput(nNodeID, nOutput, bWithOptionalY);
		}

		// IL
		public bool WriteImmediateLowOutput(ushort nNodeID, int nOutput, bool bWithOptionalY = false)
		{
			return _WriteImmediateLowOutput(nNodeID, nOutput, bWithOptionalY);
		}

		// IC
		public bool ReadImmediateCommandedCurrent(ushort nNodeID, ref double dImmediateCurrentCommanded)
		{
			return _ReadImmediateCommandedCurrent(nNodeID, ref dImmediateCurrentCommanded);
		}

		// IO
		public bool ReadOutputStatus(ushort nNodeID, ref byte chOutputStatus, bool bWithOptionalY = false)
		{
			return _ReadOutputStatus(nNodeID, ref chOutputStatus, bWithOptionalY);
		}

		public bool WriteOutputStatus(ushort nNodeID, byte chOutputStatus, bool bWithOptionalY = false)
		{
			return _WriteOutputStatus(nNodeID, chOutputStatus, bWithOptionalY);
		}

		// IP
		public bool ReadImmediatePosition(ushort nNodeID, ref int nImmediatelyPosition)
		{
			return _ReadImmediatePosition(nNodeID, ref nImmediatelyPosition);
		}

		// IQ
		public bool ReadImmediateActualCurrent(ushort nNodeID, ref double dImmediateActualCurrent)
		{
			return _ReadImmediateActualCurrent(nNodeID, ref dImmediateActualCurrent);
		}

		// IS
		public bool ReadInputStatus(ushort nNodeID, ref int nInputStatus, bool bWithOptionalY = false)
		{
			return _ReadInputStatus(nNodeID, ref nInputStatus, bWithOptionalY);
		}

		// IT
		public bool ReadImmediateTemperature(ushort nNodeID, ref double dTemperature)
		{
			return _ReadImmediateTemperature(nNodeID, ref dTemperature);
		}

		public bool ReadImmediateTemperatureWithChannel(ushort nNodeID, int nChannel, ref double dTemperature)
		{
			return _ReadImmediateTemperatureWithChannel(nNodeID, nChannel, ref dTemperature);
		}

		// IU
		public bool ReadImmediateVoltage(ushort nNodeID, ref double dVoltage)
		{
			return _ReadImmediateVoltage(nNodeID, ref dVoltage);
		}

		public bool ReadImmediateVoltageWithChannel(ushort nNodeID, int nChannel, ref double dVoltage)
		{
			return _ReadImmediateVoltageWithChannel(nNodeID, nChannel, ref dVoltage);
		}

		// IV
		public bool ReadImmediateActualVelocity(ushort nNodeID, ref double dActualVelocity)
		{
			return _ReadImmediateActualVelocity(nNodeID, ref dActualVelocity);
		}

		public bool ReadImmediateTargetVelocity(ushort nNodeID, ref double dTargetVelocity)
		{
			return _ReadImmediateTargetVelocity(nNodeID, ref dTargetVelocity);
		}

		// IX
		public bool ReadImmediatePositionError(ushort nNodeID, ref int nImmediatePositionError)
		{
			return _ReadImmediatePositionError(nNodeID, ref nImmediatePositionError);
		}

		//JA
		public bool ReadJogAcceleration(ushort nNodeID, ref double dJogAccel)
		{
			return _ReadJogAcceleration(nNodeID, ref dJogAccel);
		}

		public bool WriteJogAcceleration(ushort nNodeID, double dJogAccel)
		{
			return _WriteJogAcceleration(nNodeID, dJogAccel);
		}

		// JC
		public bool ReadVelocityModeSecondSpeed(ushort nNodeID, ref double dVelocityModeSecondSpeed)
		{
			return _ReadVelocityModeSecondSpeed(nNodeID, ref dVelocityModeSecondSpeed);
		}

		public bool WriteVelocityModeSecondSpeed(ushort nNodeID, double dVelocityModeSecondSpeed)
		{
			return _WriteVelocityModeSecondSpeed(nNodeID, dVelocityModeSecondSpeed);
		}

		// JD
		public bool WriteJogDisable(ushort nNodeID)
		{
			return _WriteJogDisable(nNodeID);
		}

		// JE
		public bool WriteJogEnable(ushort nNodeID)
		{
			return _WriteJogEnable(nNodeID);
		}

		// JL
		public bool ReadJogDeceleration(ushort nNodeID, ref double dJogDecel)
		{
			return _ReadJogDeceleration(nNodeID, ref dJogDecel);
		}

		public bool WriteJogDeceleration(ushort nNodeID, double dJogDecel)
		{
			return _WriteJogDeceleration(nNodeID, dJogDecel);
		}

		// JM
		public bool ReadJogMode(ushort nNodeID, ref byte nJogMode)
		{
			return _ReadJogMode(nNodeID, ref nJogMode);
		}

		public bool WriteJogMode(ushort nNodeID, byte nJogMode)
		{
			return _WriteJogMode(nNodeID, nJogMode);
		}

		// JS
		public bool ReadJogSpeed(ushort nNodeID, ref double dJogspeed)
		{
			return _ReadJogSpeed(nNodeID, ref dJogspeed);
		}

		public bool WriteJogSpeed(ushort nNodeID, double dJogspeed)
		{
			return _WriteJogSpeed(nNodeID, dJogspeed);
		}

		// KC
		public bool ReadOverallServoFilter(ushort nNodeID, ref int nOverallServoFilter)
		{
			return _ReadOverallServoFilter(nNodeID, ref nOverallServoFilter);
		}

		public bool WriteOverallServoFilter(ushort nNodeID, int nOverallServoFilter)
		{
			return _WriteOverallServoFilter(nNodeID, nOverallServoFilter);
		}

		// KD
		public bool ReadDifferentialConstant(ushort nNodeID, ref int nDifferentialConstant)
		{
			return _ReadDifferentialConstant(nNodeID, ref nDifferentialConstant);
		}

		public bool WriteDifferentialConstant(ushort nNodeID, int nDifferentialConstant)
		{
			return _WriteDifferentialConstant(nNodeID, nDifferentialConstant);
		}

		// KE
		public bool ReadDifferentialFilter(ushort nNodeID, ref int nDifferentialFilter)
		{
			return _ReadDifferentialFilter(nNodeID, ref nDifferentialFilter);
		}

		public bool WriteDifferentialFilter(ushort nNodeID, int nDifferentialFilter)
		{
			return _WriteDifferentialFilter(nNodeID, nDifferentialFilter);
		}

		// KF - Read
		public bool ReadVelocityFeedforwardConstant(ushort nNodeID, ref int nVelocityFeedforwardConstant)
		{
			return _ReadVelocityFeedforwardConstant(nNodeID, ref nVelocityFeedforwardConstant);
		}

		public bool WriteVelocityFeedforwardConstant(ushort nNodeID, int nVelocityFeedforwardConstant)
		{
			return _WriteVelocityFeedforwardConstant(nNodeID, nVelocityFeedforwardConstant);
		}

		// KG
		public bool ReadSecondaryGlobalGain(ushort nNodeID, ref int nSecondaryGlobalGain)
		{
			return _ReadSecondaryGlobalGain(nNodeID, ref nSecondaryGlobalGain);
		}

		public bool WriteSecondaryGlobalGain(ushort nNodeID, int nSecondaryGlobalGain)
		{
			return _WriteSecondaryGlobalGain(nNodeID, nSecondaryGlobalGain);
		}

		// KI
		public bool ReadIntegratorConstant(ushort nNodeID, ref int nIntegratorConstant)
		{
			return _ReadIntegratorConstant(nNodeID, ref nIntegratorConstant);
		}

		public bool WriteIntegratorConstant(ushort nNodeID, int nIntegratorConstant)
		{
			return _WriteIntegratorConstant(nNodeID, nIntegratorConstant);
		}

		// KJ
		public bool ReadJerkFilterFrequency(ushort nNodeID, ref int nJerkFilterFrequency)
		{
			return _ReadJerkFilterFrequency(nNodeID, ref nJerkFilterFrequency);
		}

		public bool WriteJerkFilterFrequency(ushort nNodeID, int nJerkFilterFrequency)
		{
			return _WriteJerkFilterFrequency(nNodeID, nJerkFilterFrequency);
		}

		// KK
		public bool ReadInertiaFeedforwardConstant(ushort nNodeID, ref int nInertiaFeedforwardConstant)
		{
			return _ReadInertiaFeedforwardConstant(nNodeID, ref nInertiaFeedforwardConstant);
		}

		public bool WriteInertiaFeedforwardConstant(ushort nNodeID, int nInertiaFeedforwardConstant)
		{
			return _WriteInertiaFeedforwardConstant(nNodeID, nInertiaFeedforwardConstant);
		}

		// KP
		public bool ReadProportionalConstant(ushort nNodeID, ref int nProportionalConstant)
		{
			return _ReadProportionalConstant(nNodeID, ref nProportionalConstant);
		}

		public bool WriteProportionalConstant(ushort nNodeID, int nProportionalConstant)
		{
			return _WriteProportionalConstant(nNodeID, nProportionalConstant);
		}

		// KV
		public bool ReadVelocityFeedbackConstant(ushort nNodeID, ref int nVelocityFeedbackConstant)
		{
			return _ReadVelocityFeedbackConstant(nNodeID, ref nVelocityFeedbackConstant);
		}

		public bool WriteVelocityFeedbackConstant(ushort nNodeID, int nVelocityFeedbackConstant)
		{
			return _WriteVelocityFeedbackConstant(nNodeID, nVelocityFeedbackConstant);
		}

		// LA
		public bool ReadLeadAngleMaxValue(ushort nNodeID, ref byte nLeadAngleMaxValue)
		{
			return _ReadLeadAngleMaxValue(nNodeID, ref nLeadAngleMaxValue);
		}

		public bool WriteLeadAngleMaxValue(ushort nNodeID, byte nLeadAngleMaxValue)
		{
			return _WriteLeadAngleMaxValue(nNodeID, nLeadAngleMaxValue);
		}

		// LM
		public bool ReadSoftwareLimitCCW(ushort nNodeID, ref int nSoftwareLimitCCW)
		{
			return _ReadSoftwareLimitCCW(nNodeID, ref nSoftwareLimitCCW);
		}

		public bool WriteSoftwareLimitCCW(ushort nNodeID, int nSoftwareLimitCCW)
		{
			return _WriteSoftwareLimitCCW(nNodeID, nSoftwareLimitCCW);
		}

		// LP
		public bool ReadSoftwareLimitCW(ushort nNodeID, ref int nSoftwareLimitCW)
		{
			return _ReadSoftwareLimitCW(nNodeID, ref nSoftwareLimitCW);
		}

		public bool WriteSoftwareLimitCW(ushort nNodeID, int nSoftwareLimitCW)
		{
			return _WriteSoftwareLimitCW(nNodeID, nSoftwareLimitCW);
		}

		// LS
		public bool ReadLeadAngleSpeed(ushort nNodeID, ref double nLeadAngleSpeed)
		{
			return _ReadLeadAngleSpeed(nNodeID, ref nLeadAngleSpeed);
		}

		public bool WriteLeadAngleSpeed(ushort nNodeID, double nLeadAngleSpeed)
		{
			return _WriteLeadAngleSpeed(nNodeID, nLeadAngleSpeed);
		}

		// LV
		public bool ReadLowVoltageThreshold(ushort nNodeID, ref int nLowVoltageThreshold)
		{
			return _ReadLowVoltageThreshold(nNodeID, ref nLowVoltageThreshold);
		}

		public bool WriteLowVoltageThreshold(ushort nNodeID, int nLowVoltageThreshold)
		{
			return _WriteLowVoltageThreshold(nNodeID, nLowVoltageThreshold);
		}

		// MD
		public bool WriteMotorDisable(ushort nNodeID)
		{
			return _WriteMotorDisable(nNodeID);
		}

		// ME
		public bool WriteMotorEnable(ushort nNodeID)
		{
			return _WriteMotorEnable(nNodeID);
		}

		// MO
		public bool ReadMotionOutput(ushort nNodeID, ref byte nOutputUsage)
		{
			return _ReadMotionOutput(nNodeID, ref nOutputUsage);
		}

		public bool WriteMotionOutput(ushort nNodeID, byte nOutputUsage)
		{
			return _WriteMotionOutput(nNodeID, nOutputUsage);
		}

		public bool ReadMotionOutputFlexIO(ushort nNodeID, ref byte nOutputUsage, ref byte nOutput)
		{
			return _ReadMotionOutputFlexIO(nNodeID, ref nOutputUsage, ref nOutput);
		}

		public bool WriteMotionOutputFlexIO(ushort nNodeID, byte nOutputUsage, byte nOutput)
		{
			return _WriteMotionOutputFlexIO(nNodeID, nOutputUsage, nOutput);
		}

		public bool ReadMotionOutputMSeries(ushort nNodeID, byte nOutput, ref byte nOutputUsage)
		{
			return _ReadMotionOutputMSeries(nNodeID, nOutput, ref nOutputUsage);
		}

		public bool WriteMotionOutputMSeries(ushort nNodeID, byte nOutput, byte nOutputUsage)
		{
			return _WriteMotionOutputMSeries(nNodeID, nOutput, nOutputUsage);
		}

		// MR
		public bool ReadMicrostepResolution(ushort nNodeID, ref byte nMicrostepResolution)
		{
			return _ReadMicrostepResolution(nNodeID, ref nMicrostepResolution);
		}

		public bool WriteMicrostepResolution(ushort nNodeID, byte nMicrostepResolution)
		{
			return _WriteMicrostepResolution(nNodeID, nMicrostepResolution);
		}

		// MT
		public bool ReadMultiTasking(ushort nNodeID, ref byte nMultiTasking)
		{
			return _ReadMultiTasking(nNodeID, ref nMultiTasking);
		}

		public bool WriteMultiTasking(ushort nNodeID, byte nMultiTasking)
		{
			return _WriteMultiTasking(nNodeID, nMultiTasking);
		}

		// MV
		public bool ReadModelRevision(ushort nNodeID, ref string strModelRevision)
		{
			IntPtr strIntPtr = IntPtr.Zero;
			bool ret = _ReadModelRevision(nNodeID, ref strIntPtr);
			strModelRevision = Marshal.PtrToStringAnsi(strIntPtr);
			return ret;
		}

		// OF
		public bool WriteOnFault(ushort nNodeID, byte nQSegment)
		{
			return _WriteOnFault(nNodeID, nQSegment);
		}

		// OI
		public bool WriteOnInput(ushort nNodeID, byte? nInputSensor, char? chInputStatus, bool bWithOptionalX = false)
		{
			IntPtr ptrInputSensor = IntPtr.Zero;
			if (nInputSensor.HasValue)
			{
				ptrInputSensor = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nInputSensor.Value, ptrInputSensor, true);
			}
			IntPtr ptrInputStatus = IntPtr.Zero;
			if (chInputStatus.HasValue)
			{
				ptrInputStatus = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chInputStatus.Value, ptrInputStatus, true);
			}
			bool ret = _WriteOnInput(nNodeID, ptrInputSensor, ptrInputStatus, bWithOptionalX);

			Marshal.FreeHGlobal(ptrInputSensor);
			Marshal.FreeHGlobal(ptrInputStatus);

			return ret;
		}

		// OP
		public bool ReadOptionBoard(ushort nNodeID, ref byte nOptionBoard)
		{
			return _ReadOptionBoard(nNodeID, ref nOptionBoard);
		}

		// PA
		public bool ReadPowerupAccelerationCurrent(ushort nNodeID, ref double dPowerupAccelerationCurrent)
		{
			return _ReadPowerupAccelerationCurrent(nNodeID, ref dPowerupAccelerationCurrent);
		}

		public bool WritePowerupAccelerationCurrent(ushort nNodeID, double dPowerupAccelerationCurrent)
		{
			return _WritePowerupAccelerationCurrent(nNodeID, dPowerupAccelerationCurrent);
		}

		// PC
		public bool ReadPowerupCurrent(ushort nNodeID, ref double dPowerupCurrent)
		{
			return _ReadPowerupCurrent(nNodeID, ref dPowerupCurrent);
		}

		public bool WritePowerupCurrent(ushort nNodeID, double dPowerupCurrent)
		{
			return _WritePowerupCurrent(nNodeID, dPowerupCurrent);
		}

		// PD
		public bool ReadInPositionCounts(ushort nNodeID, ref int nInPositionCounts)
		{
			return _ReadInPositionCounts(nNodeID, ref nInPositionCounts);
		}

		public bool WriteInPositionCounts(ushort nNodeID, int nInPositionCounts)
		{
			return _WriteInPositionCounts(nNodeID, nInPositionCounts);
		}

		// PE
		public bool ReadInPositionTiming(ushort nNodeID, ref int nInPositionTiming)
		{
			return _ReadInPositionTiming(nNodeID, ref nInPositionTiming);
		}

		public bool WriteInPositionTiming(ushort nNodeID, int nInPositionTiming)
		{
			return _WriteInPositionTiming(nNodeID, nInPositionTiming);
		}

		// PF
		public bool ReadPositionFault(ushort nNodeID, ref int nPositionFault)
		{
			return _ReadPositionFault(nNodeID, ref nPositionFault);
		}

		public bool WritePositionFault(ushort nNodeID, int nPositionFault)
		{
			return _WritePositionFault(nNodeID, nPositionFault);
		}

		// PH
		public bool ReadInhibitionOfPulseCommand(ushort nNodeID, ref int nInhibitionOfPulseCommand)
		{
			return _ReadInhibitionOfPulseCommand(nNodeID, ref nInhibitionOfPulseCommand);
		}

		public bool WriteInhibitionOfPulseCommand(ushort nNodeID, int nInhibitionOfPulseCommand)
		{
			return _WriteInhibitionOfPulseCommand(nNodeID, nInhibitionOfPulseCommand);
		}

		// PI
		public bool ReadPowerupIdleCurrent(ushort nNodeID, ref double dPowerupIdleCurrent)
		{
			return _ReadPowerupIdleCurrent(nNodeID, ref dPowerupIdleCurrent);
		}

		public bool WritePowerupIdleCurrent(ushort nNodeID, double dPowerupIdleCurrent)
		{
			return _WritePowerupIdleCurrent(nNodeID, dPowerupIdleCurrent);
		}

		// PK
		public bool ReadParameterLock(ushort nNodeID, ref int nParameterLock)
		{
			return _ReadParameterLock(nNodeID, ref nParameterLock);
		}

		public bool WriteParameterLock(ushort nNodeID, int nParameterLock)
		{
			return _WriteParameterLock(nNodeID, nParameterLock);
		}

		// PL
		public bool ReadPositionLimit(ushort nNodeID, ref int nPositionLimit)
		{
			return _ReadPositionLimit(nNodeID, ref nPositionLimit);
		}

		public bool WritePositionLimit(ushort nNodeID, int nPositionLimit)
		{
			return _WritePositionLimit(nNodeID, nPositionLimit);
		}

		// PM
		public bool ReadPowerupMode(ushort nNodeID, ref byte nPowerupMode)
		{
			return _ReadPowerupMode(nNodeID, ref nPowerupMode);
		}

		public bool WritePowerupMode(ushort nNodeID, byte nPowerupMode)
		{
			return _WritePowerupMode(nNodeID, nPowerupMode);
		}

		// PN
		public bool WriteProbeonDemand(ushort nNodeID)
		{
			return _WriteProbeonDemand(nNodeID);
		}

		// PP
		public bool ReadPowerupPeakCurrent(ushort nNodeID, ref double dPowerupPeakCurrent)
		{
			return _ReadPowerupPeakCurrent(nNodeID, ref dPowerupPeakCurrent);
		}

		public bool WritePowerupPeakCurrent(ushort nNodeID, double dPowerupPeakCurrent)
		{
			return _WritePowerupPeakCurrent(nNodeID, dPowerupPeakCurrent);
		}

		// PR
		public bool ReadProtocol(ushort nNodeID, ref byte nProtocol)
		{
			return _ReadProtocol(nNodeID, ref nProtocol);
		}

		// PT
		public bool ReadPulseType(ushort nNodeID, ref byte nPulseType)
		{
			return _ReadPulseType(nNodeID, ref nPulseType);
		}

		public bool WritePulseType(ushort nNodeID, byte nPulseType)
		{
			return _WritePulseType(nNodeID, nPulseType);
		}

		// PV
		public bool ReadSecondaryElectronicGearing(ushort nNodeID, ref byte nSecondaryElectronicGearing)
		{
			return _ReadSecondaryElectronicGearing(nNodeID, ref nSecondaryElectronicGearing);
		}

		public bool WriteSecondaryElectronicGearing(ushort nNodeID, byte nSecondaryElectronicGearing)
		{
			return _WriteSecondaryElectronicGearing(nNodeID, nSecondaryElectronicGearing);
		}

		// QE
		public bool WriteQueueExecute(ushort nNodeID)
		{
			return _WriteQueueExecute(nNodeID);
		}

		// QK
		public bool WriteQueueKill(ushort nNodeID)
		{
			return _WriteQueueKill(nNodeID);
		}

		// QX
		public bool WriteQueueLoadAndExecute(ushort nNodeID, byte nQSegment)
		{
			return _WriteQueueLoadAndExecute(nNodeID, nQSegment);
		}

		// RC
		public bool WriteRegisterCounter(ushort nNodeID, byte? nInputSensor, char? chInputStatus, bool bWithOptionalX = false)
		{
			IntPtr ptrInputSensor = IntPtr.Zero;
			if (nInputSensor.HasValue)
			{
				ptrInputSensor = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nInputSensor.Value, ptrInputSensor, true);
			}
			IntPtr ptrInputStatus = IntPtr.Zero;
			if (chInputStatus.HasValue)
			{
				ptrInputStatus = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(char)));
				Marshal.StructureToPtr(chInputStatus.Value, ptrInputStatus, true);
			}

			bool ret = _WriteRegisterCounter(nNodeID, ptrInputSensor, ptrInputStatus, bWithOptionalX);

			Marshal.FreeHGlobal(ptrInputSensor);
			Marshal.FreeHGlobal(ptrInputStatus);

			return ret;
		}

		// RD
		public bool WriteRegisterDecrement(ushort nNodeID, char chDataRegister)
		{
			return _WriteRegisterDecrement(nNodeID, chDataRegister);
		}

		// RE
		public bool WriteRestart(ushort nNodeID)
		{
			return _WriteRestart(nNodeID);
		}

		// RI
		public bool WriteRegisterIncrement(ushort nNodeID, char chDataRegister)
		{
			return _WriteRegisterIncrement(nNodeID, chDataRegister);
		}

		// RL
		public bool ReadRegisterLoad(ushort nNodeID, char chRegisterAddress, ref int nRegisterValue, bool bImmediately)
		{
			return _ReadRegisterLoad(nNodeID, chRegisterAddress, ref nRegisterValue, bImmediately);
		}

		public bool WriteRegisterLoad(ushort nNodeID, char chRegisterAddress, int nRegisterValue, bool bImmediately)
		{
			return _WriteRegisterLoad(nNodeID, chRegisterAddress, nRegisterValue, bImmediately);
		}

		// RM
		public bool WriteRegisterMove(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteRegisterMove(nNodeID, chRegister1, chRegister2);
		}

		// RO
		public bool ReadAntiResonanceOn(ushort nNodeID, ref bool bAntiResonanceOn)
		{
			return _ReadAntiResonanceOn(nNodeID, ref bAntiResonanceOn);
		}

		public bool WriteAntiResonanceOn(ushort nNodeID, bool bAntiResonanceOn)
		{
			return _WriteAntiResonanceOn(nNodeID, bAntiResonanceOn);
		}

		// RR
		public bool WriteRegisterRead(ushort nNodeID, char chRegister, byte nLocation)
		{
			return _WriteRegisterRead(nNodeID, chRegister, nLocation);
		}

		// RS
		public bool ReadRequestStatus(ushort nNodeID, ref string strStatusWord)
		{
			IntPtr strIntPtr = IntPtr.Zero;
			bool ret = _ReadRequestStatus(nNodeID, ref strIntPtr);
			strStatusWord = Marshal.PtrToStringAnsi(strIntPtr);
			return ret;
		}

		// RV
		public bool ReadRevisionLevel(ushort nNodeID, ref byte strRevision)
		{
			return _ReadRevisionLevel(nNodeID, ref strRevision);
		}

		// RW
		public bool WriteRegisterWrite(ushort nNodeID, char chRegister1, byte chRegister2)
		{
			return _WriteRegisterWrite(nNodeID, chRegister1, chRegister2);
		}

		// R+
		public bool WriteRegisterAdd(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteRegisterAdd(nNodeID, chRegister1, chRegister2);
		}

		// R-
		public bool WriteRegisterSubstract(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteRegisterSubstract(nNodeID, chRegister1, chRegister2);
		}

		// R*
		public bool WriteRegisterMultiply(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteRegisterMultiply(nNodeID, chRegister1, chRegister2);
		}

		// R/
		public bool WriteRegisterDivide(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteRegisterDivide(nNodeID, chRegister1, chRegister2);
		}

		// R&
		public bool WriteRegisterAnd(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteRegisterAnd(nNodeID, chRegister1, chRegister2);
		}

		// R|
		public bool WriteRegisterOr(ushort nNodeID, char chRegister1, char chRegister2)
		{
			return _WriteRegisterOr(nNodeID, chRegister1, chRegister2);
		}

		// SA
		public bool WriteSaveParameters(ushort nNodeID)
		{
			return _WriteSaveParameters(nNodeID);
		}

		// SC
		public bool ReadStatusCode(ushort nNodeID, ref int nStatusCode)
		{
			return _ReadStatusCode(nNodeID, ref nStatusCode);
		}

		// SD
		public bool ReadSetDirection(ushort nNodeID, ref byte nDirection)
		{
			return _ReadSetDirection(nNodeID, ref nDirection);
		}

		public bool WriteSetDirection(ushort nNodeID, byte nInputSensor, char nDirection)
		{
			return _WriteSetDirection(nNodeID, nInputSensor, nDirection);
		}

		// SF
		public bool ReadStepFilterFrequency(ushort nNodeID, ref int nFilter)
		{
			return _ReadStepFilterFrequency(nNodeID, ref nFilter);
		}

		public bool WriteStepFilterFrequency(ushort nNodeID, int nFilter)
		{
			return _WriteStepFilterFrequency(nNodeID, nFilter);
		}

		// SH
		public bool WriteSeekHome(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteSeekHome(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// SI
		public bool ReadEnableInputUsage(ushort nNodeID, ref byte nInputUsage)
		{
			return _ReadEnableInputUsage(nNodeID, ref nInputUsage);
		}

		public bool WriteEnableInputUsage(ushort nNodeID, byte nInputUsage)
		{
			return _WriteEnableInputUsage(nNodeID, nInputUsage);
		}

		public bool ReadEnableInputUsageFlexIO(ushort nNodeID, ref byte nInputUsage, ref byte nInputSensor)
		{
			return _ReadEnableInputUsageFlexIO(nNodeID, ref nInputUsage, ref nInputSensor);
		}

		public bool WriteEnableInputUsageFlexIO(ushort nNodeID, byte nInputUsage, byte nInputSensor)
		{
			return _WriteEnableInputUsageFlexIO(nNodeID, nInputUsage, nInputSensor);
		}

		//SJ
		public bool WriteStopJogging(ushort nNodeID)
		{
			return _WriteStopJogging(nNodeID);
		}

		// SK
		public bool WriteStopAndKill(ushort nNodeID, bool bWithOptionalD = false)
		{
			return _WriteStopAndKill(nNodeID, bWithOptionalD);
		}

		// SM
		public bool WriteStopMove(ushort nNodeID, char chStopMode)
		{
			return _WriteStopMove(nNodeID, chStopMode);
		}

		// SO
		public bool WriteSetOutput(ushort nNodeID, int nOutput, char chOutputStatus, bool bWithOptionalY = false)
		{
			return _WriteSetOutput(nNodeID, nOutput, chOutputStatus, bWithOptionalY);
		}

		// SP
		public bool ReadSetPosition(ushort nNodeID, ref int nSetPosition)
		{
			return _ReadSetPosition(nNodeID, ref nSetPosition);
		}

		public bool WriteSetPosition(ushort nNodeID, int nSetPosition)
		{
			return _WriteSetPosition(nNodeID, nSetPosition);
		}

		// ST
		public bool WriteStop(ushort nNodeID, bool bWithOptionalD = false)
		{
			return _WriteStop(nNodeID, bWithOptionalD);
		}

		// TD
		public bool ReadTransmitDelay(ushort nNodeID, ref int nTransmitDelay)
		{
			return _ReadTransmitDelay(nNodeID, ref nTransmitDelay);
		}

		public bool WriteTransmitDelay(ushort nNodeID, int nTransmitDelay)
		{
			return _WriteTransmitDelay(nNodeID, nTransmitDelay);
		}

		// TI
		public bool WriteTestInput(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteTestInput(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// TO
		public bool ReadTachOutput(ushort nNodeID, ref int nTachOutput)
		{
			return _ReadTachOutput(nNodeID, ref nTachOutput);
		}

		public bool WriteTachOutput(ushort nNodeID, int nTachOutput)
		{
			return _WriteTachOutput(nNodeID, nTachOutput);
		}

		// TR
		public bool WriteTestRegister(ushort nNodeID, char chDataRegister, int nRegisterValue)
		{
			return _WriteTestRegister(nNodeID, chDataRegister, nRegisterValue);
		}

		// TS
		public bool WriteTimeStamp(ushort nNodeID)
		{
			return _WriteTimeStamp(nNodeID);
		}

		// TT
		public bool ReadPulseCompleteTiming(ushort nNodeID, ref int nPulseCompleteTiming)
		{
			return _ReadPulseCompleteTiming(nNodeID, ref nPulseCompleteTiming);
		}

		public bool WritePulseCompleteTiming(ushort nNodeID, int nPulseCompleteTiming)
		{
			return _WritePulseCompleteTiming(nNodeID, nPulseCompleteTiming);
		}

		// TV
		public bool ReadTorqueRipple(ushort nNodeID, ref double nTorqueRipple)
		{
			return _ReadTorqueRipple(nNodeID, ref nTorqueRipple);
		}

		public bool WriteTorqueRipple(ushort nNodeID, double nTorqueRipple)
		{
			return _WriteTorqueRipple(nNodeID, nTorqueRipple);
		}

		// VC
		public bool ReadVelocityChange(ushort nNodeID, ref double dVelocityChange)
		{
			return _ReadVelocityChange(nNodeID, ref dVelocityChange);
		}

		public bool WriteVelocityChange(ushort nNodeID, double dVelocityChange)
		{
			return _WriteVelocityChange(nNodeID, dVelocityChange);
		}

		// VE
		public bool ReadVelocity(ushort nNodeID, ref double dVelocity)
		{
			return _ReadVelocity(nNodeID, ref dVelocity);
		}

		public bool WriteVelocity(ushort nNodeID, double dVelocity)
		{
			return _WriteVelocity(nNodeID, dVelocity);
		}

		// VI
		public bool ReadVelocityIntegratorConstant(ushort nNodeID, ref int nVelocityIntegratorConstant)
		{
			return _ReadVelocityIntegratorConstant(nNodeID, ref nVelocityIntegratorConstant);
		}

		public bool WriteVelocityIntegratorConstant(ushort nNodeID, int nVelocityIntegratorConstant)
		{
			return _WriteVelocityIntegratorConstant(nNodeID, nVelocityIntegratorConstant);
		}

		// VL
		public bool ReadVoltageLimit(ushort nNodeID, ref int nVoltageLimit)
		{
			return _ReadVoltageLimit(nNodeID, ref nVoltageLimit);
		}

		public bool WriteVoltageLimit(ushort nNodeID, int nVoltageLimit)
		{
			return _WriteVoltageLimit(nNodeID, nVoltageLimit);
		}

		// VM
		public bool ReadMaximumVelocity(ushort nNodeID, ref double dMaximumVelocity)
		{
			return _ReadMaximumVelocity(nNodeID, ref dMaximumVelocity);
		}

		public bool WriteMaximumVelocity(ushort nNodeID, double dMaximumVelocity)
		{
			return _WriteMaximumVelocity(nNodeID, dMaximumVelocity);
		}

		// VP
		public bool ReadVelocityProportionalConstant(ushort nNodeID, ref int nVelocityProportionalConstant)
		{
			return _ReadVelocityProportionalConstant(nNodeID, ref nVelocityProportionalConstant);
		}

		public bool WriteVelocityProportionalConstant(ushort nNodeID, int nVelocityProportionalConstant)
		{
			return _WriteVelocityProportionalConstant(nNodeID, nVelocityProportionalConstant);
		}

		// VR
		public bool ReadVelocityRipple(ushort nNodeID, ref double nVelocityRipple)
		{
			return _ReadVelocityRipple(nNodeID, ref nVelocityRipple);
		}

		public bool WriteVelocityRipple(ushort nNodeID, double nVelocityRipple)
		{
			return _WriteVelocityRipple(nNodeID, nVelocityRipple);
		}

		// WD
		public bool WriteWaitDelay(ushort nNodeID, char chDataRegister)
		{
			return _WriteWaitDelay(nNodeID, chDataRegister);
		}

		// WI
		public bool WriteWaitforInput(ushort nNodeID, byte nInputSensor, char chInputStatus, bool bWithOptionalX = false)
		{
			return _WriteWaitforInput(nNodeID, nInputSensor, chInputStatus, bWithOptionalX);
		}

		// WM
		public bool WriteWaitonMove(ushort nNodeID)
		{
			return _WriteWaitonMove(nNodeID);
		}

		// WP
		public bool WriteWaitPosition(ushort nNodeID)
		{
			return _WriteWaitPosition(nNodeID);
		}

		// WT
		public bool WriteWaitTime(ushort nNodeID, double dWaitTime)
		{
			return _WriteWaitTime(nNodeID, dWaitTime);
		}

		// ZC
		public bool ReadRegenResistorWattage(ushort nNodeID, ref int nRegenResistorWattage)
		{
			return _ReadRegenResistorWattage(nNodeID, ref nRegenResistorWattage);
		}

		public bool WriteRegenResistorWattage(ushort nNodeID, int nRegenResistorWattage)
		{
			return _WriteRegenResistorWattage(nNodeID, nRegenResistorWattage);
		}

		// ZR
		public bool ReadRegenResistorValue(ushort nNodeID, ref int nRegenResistorValue)
		{
			return _ReadRegenResistorValue(nNodeID, ref nRegenResistorValue);
		}

		public bool WriteRegenResistorValue(ushort nNodeID, int nRegenResistorValue)
		{
			return _WriteRegenResistorValue(nNodeID, nRegenResistorValue);
		}

		// ZT
		public bool ReadRegenResistorPeakTime(ushort nNodeID, ref int nRegenResistorPeakTime)
		{
			return _ReadRegenResistorPeakTime(nNodeID, ref nRegenResistorPeakTime);
		}

		public bool WriteRegenResistorPeakTime(ushort nNodeID, int nRegenResistorPeakTime)
		{
			return _WriteRegenResistorPeakTime(nNodeID, nRegenResistorPeakTime);
		}

		// ZE
		public bool ReadWatchDogEnable(ushort nNodeID, ref bool bEnable)
		{
			return _ReadWatchDogEnable(nNodeID, ref bEnable);
		}

		public bool WriteWatchDogEnable(ushort nNodeID, bool bEnable)
		{
			return _WriteWatchDogEnable(nNodeID, bEnable);
		}

		// ZS
		public bool ReadWatchDogTimeOut(ushort nNodeID, ref ushort bTimeOut)
		{
			return _ReadWatchDogTimeOut(nNodeID, ref bTimeOut);
		}

		public bool WriteWatchDogTimeOut(ushort nNodeID, ushort bTimeOut)
		{
			return _WriteWatchDogTimeOut(nNodeID, bTimeOut);
		}

		// ZA
		public bool ReadWatchDogConfig(ushort nNodeID, ref bool bFaultOutput, ref byte bConfig)
		{
			return _ReadWatchDogConfig(nNodeID, ref bFaultOutput, ref bConfig);
		}

		public bool WriteWatchDogConfig(ushort nNodeID, bool bFaultOutput, byte bConfig)
		{
			return _WriteWatchDogConfig(nNodeID, bFaultOutput, bConfig);
		}

		
	

		#endregion
	}
}
