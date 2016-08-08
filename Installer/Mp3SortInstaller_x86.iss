; -- Mp3SortInstaller.iss --
[Setup]
AppName=Mp3Sort
AppId=Growup_Consultant/Software/Mp3Sort
AppPublisher=Growup Consultant
AppCopyright=Growup Consultant
AppVerName=Mp3Sort 1.0.0.0
AppVersion=1.0.0.0
ArchitecturesAllowed=x86 x64
DefaultDirName={pf}\Mp3Sort
DefaultGroupName=Mp3Sort
UninstallDisplayIcon={app}\Mp3Sort.exe
ShowLanguageDialog=no
VersionInfoVersion=1.0.0.0
VersionInfoDescription=Mp3Sortセットアッププログラム

OutputBaseFilename=Mp3SortInstaller_x86
OutputDir="."
SetupIconFile="..\Mp3Sort\Mp3Sort.ico"

[Tasks]
Name: desktopicon; Description: "デスクトップにショートカットアイコンを作成する";

[Files]
Source: "..\Mp3Sort\bin\Release\Mp3Sort.exe";          DestDir: "{app}"; Flags: ignoreversion
Source: "..\Mp3Sort\bin\Release\Mp3Sort.config";       DestDir: "{app}"; Flags: ignoreversion
Source: "..\Mp3Sort\bin\Release\Interop.Shell32.dll";  DestDir: "{app}"; Flags: ignoreversion
Source: "..\Mp3Sort\bin\Release\NexFx.dll";            DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Mp3Sort"; Filename: "{app}\Mp3Sort.exe"
Name: "{commondesktop}\Mp3Sort"; Filename: "{app}\Mp3Sort.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Languages]
Name: japanese; MessagesFile: compiler:Languages\Japanese.isl

[Run]
Filename: "{app}\Mp3Sort.exe"; Description: "アプリケーションを起動する"; Flags: postinstall shellexec skipifsilent
