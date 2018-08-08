#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

	;  --------------------------
	; | AutoSkype                |
	; | Version 1.0              |
	; | Matt Palmer 2018, ITWorks|
	;  --------------------------
	; (happy hacking)

	; -----------------------------
	; load configuration from files
	; -----------------------------

	; set initial config file locations
	initFileName := "initconfig.txt"
	configFileName := "config.txt"
	
	; -------------------
	; load initial config
	; -------------------
	
	IfNotExist, %A_AppData%\AutoSkype\%initFileName%
	{
		MsgBox Cannot find initialization config file in %A_AppData%\AutoSkype directory - exiting.
		ExitApp
	}
	
	initContents := Object()
	Loop, read, %A_AppData%\AutoSkype\%initFileName%
	{
		initContents[A_Index] := A_LoopReadLine
	}
	sharedConfigLocation := initContents[1]
	meetingRecordingSaveDirectory := initContents[2]
	
	; --------------------------------
	; copy config to current directory
	; --------------------------------
	
	; as working directory A_Scriptdir doesn't end with a backslash it must be inserted
	FileDelete, %A_ScriptDir%\%configFileName%
	
	IfExist,%sharedConfigLocation%%configFileName%
		FileCopy, %sharedConfigLocation%%configFileName%,  %A_ScriptDir%
	IfNotExist, %sharedConfigLocation%%configFileName%
		{
		MsgBox Cannot find config file at %sharedConfigLocation% - exiting.
		ExitApp
		}
	
	; ---------------------------
	; read contents of config.txt
	; ---------------------------
	
	configContents := Object()
	Loop, read, %A_ScriptDir%\%configFileName%
	{
		configContents[A_Index] := A_LoopReadLine
	}	
	meetingURL := configContents[1]
	lecturerName := configContents[2]
	finalFileLocation := configContents[3]
							
	; -------------------------
	; initialize Skype4Business
	; -------------------------
		
	IfWinNotExist, Conversation
	{	
		; this seems to be the only way to launch to a meeting url
		; lync.exe will not accept a meet url
		run "iexplore"  %meetingURL%
		; wait until conversation window appears
		WinWaitActive, Conversation
		
		; WAIT: Time between conversation window and call dialog box
		Sleep, 2000
		SendInput, {Enter}
	}
	
	WinActivate, Conversation
	WinMaximize, Conversation
		
	; Request to begin recording has been removed - to allow again, uncomment following three lines
	; MsgBox, 4,, Would you like to begin recording? (press Yes or No)
	; IfMsgBox No
	;   	ExitApp
	
	; WAIT: Time for conversation window to set up - may vary from computer to computer
	Sleep, 8000
	
	; ---------------
	; begin recording
	; ---------------
	
	DetectHiddenText, On
	
	SendInput {home}
	SendInput {lshift down}
	SendInput {tab}
	SendInput {lshift up}
	SendInput {Enter}
	SendInput {Enter}
	
	; ------------
	; share screen
	; ------------
	
	SendInput {home}
	SendInput {lshift down}
	SendInput {tab}
	SendInput {tab}
	SendInput {tab}
	SendInput {tab}
	SendInput {tab}
	SendInput {tab}
	SendInput {lshift up}
	SendInput {Enter}
	SendInput {Enter}
	SendInput {Enter}
	
	; ---------------
	; cease recording
	; ---------------
	
	Sleep 500 ; wait a moment to avoid other popups from Conference window
	MsgBox Press <scroll lock> to stop recording
	
	Keywait, ScrollLock, D
	WinActivate, Conversation
	
	SendInput {home}
	SendInput {lshift down}
	SendInput {tab}
	SendInput {tab}
	SendInput {tab}
	SendInput {lshift up}
	SendInput {Enter}
	
	; -----------------------------------
	; send recording to correct directory
	; -----------------------------------
	
	; some time is necessary before file placed into video directory for copying - no way to get around this
	MsgBox Skype For Business takes some time to process videos - When notified that file is ready, click OK to continue and latest file will be copied.
	
	run cmd.exe
	sleep 500
	
	SendInput copylatestfile{Enter}
	SendInput %meetingRecordingSaveDirectory%{Enter}
	SendInput %finalFileLocation%{Enter}
	SendInput exit{Enter}
	
	; ---------------------------
	; program termination message
	; ---------------------------
	
	MsgBox AutoSkype is done.