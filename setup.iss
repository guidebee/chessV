[Setup]
AppName=ChessV
AppVersion=2.0
AppVerName=Chess V 2.0 Release Candidate 2
ArchitecturesInstallIn64BitMode=x64
DefaultDirName={pf}\ChessV2
DefaultGroupName=Games
OutputBaseFilename=ChessV2.0-RC2-Install
AppCopyright=COPYRIGHT (C) 2012-2017 BY GREG STRONG
LicenseFile={#file AddBackslash(SourcePath) + "LICENSE.txt"}

[Dirs]
Name: "{app}\Include"
Name: "{app}\Engines"
Name: "{app}\Engines\XBoard"
Name: "{app}\Graphics"
Name: "{app}\Graphics\Piece Sets"
Name: "{app}\Graphics\Textures"
Name: "{app}\Graphics\Themes"

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"

[Files]
Source: "ChessV.exe"; DestDir: "{app}"
Source: "*.dll"; DestDir: "{app}"
Source: "LICENSE.txt"; DestDir: "{app}"
Source: "Engines\XBoard\Fairy-Max\*"; DestDir: "{app}\Engines\XBoard\Fairy-Max"
Source: "Include\*"; DestDir: "{app}\Include"
Source: "Graphics\Piece Sets\Abstract\*"; DestDir: "{app}\Graphics\Piece Sets\Abstract"
Source: "Graphics\Piece Sets\Eurasian\*"; DestDir: "{app}\Graphics\Piece Sets\Eurasian"
Source: "Graphics\Piece Sets\Runes\*"; DestDir: "{app}\Graphics\Piece Sets\Runes"
Source: "Graphics\Piece Sets\Small\*"; DestDir: "{app}\Graphics\Piece Sets\Small"
Source: "Graphics\Piece Sets\Standard\*"; DestDir: "{app}\Graphics\Piece Sets\Standard"
Source: "Graphics\Textures\Blue Marble\*"; DestDir: "{app}\Graphics\Textures\Blue Marble"
Source: "Graphics\Textures\Brownish Green Marble\*"; DestDir: "{app}\Graphics\Textures\Brownish Green Marble"
Source: "Graphics\Textures\Cloudy Marble\*"; DestDir: "{app}\Graphics\Textures\Cloudy Marble"
Source: "Graphics\Textures\Creamy Clouds\*"; DestDir: "{app}\Graphics\Textures\Creamy Clouds"
Source: "Graphics\Textures\Dark Marble\*"; DestDir: "{app}\Graphics\Textures\Dark Marble"
Source: "Graphics\Textures\Dark Metal\*"; DestDir: "{app}\Graphics\Textures\Dark Metal"
Source: "Graphics\Textures\Dark Wood\*"; DestDir: "{app}\Graphics\Textures\Dark Wood"
Source: "Graphics\Textures\Deep Purple Marble\*"; DestDir: "{app}\Graphics\Textures\Deep Purple Marble"
Source: "Graphics\Textures\Gray Marble\*"; DestDir: "{app}\Graphics\Textures\Gray Marble"
Source: "Graphics\Textures\Greenish Marble\*"; DestDir: "{app}\Graphics\Textures\Greenish Marble"
Source: "Graphics\Textures\Light Marble\*"; DestDir: "{app}\Graphics\Textures\Light Marble"
Source: "Graphics\Textures\Light Metal\*"; DestDir: "{app}\Graphics\Textures\Light Metal"
Source: "Graphics\Textures\Light Wood\*"; DestDir: "{app}\Graphics\Textures\Light Wood"
Source: "Graphics\Textures\Orange Marble\*"; DestDir: "{app}\Graphics\Textures\Orange Marble"
Source: "Graphics\Textures\Pink Marble\*"; DestDir: "{app}\Graphics\Textures\Pink Marble"
Source: "Graphics\Textures\Rose Marble\*"; DestDir: "{app}\Graphics\Textures\Rose Marble"
Source: "Graphics\Textures\Rusted Metal\*"; DestDir: "{app}\Graphics\Textures\Rusted Metal"
Source: "Graphics\Textures\Smoke\*"; DestDir: "{app}\Graphics\Textures\Smoke"
Source: "Graphics\Textures\Stone\*"; DestDir: "{app}\Graphics\Textures\Stone"
Source: "Graphics\Textures\White Marble\*"; DestDir: "{app}\Graphics\Textures\White Marble"
Source: "Graphics\Textures\Wine Marble\*"; DestDir: "{app}\Graphics\Textures\Wine Marble"
Source: "Graphics\Themes\Eurasian Chess\*"; DestDir: "{app}\Graphics\Themes\Eurasian Chess"

[Icons]
Name: "{group}\ChessV 2.0"; Filename: "{app}\ChessV.exe"; WorkingDir: "{app}"
Name: "{commondesktop}\ChessV 2.0"; Filename: "{app}\ChessV.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Code]
function IsDotNetDetected(version: string; service: cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
    key: string;
    install, serviceCount: cardinal;
    success: boolean;
begin
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
    // .NET 3.0 uses value InstallSuccess in subkey Setup
    if Pos('v3.0', version) = 1 then begin
        success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
    end else begin
        success := RegQueryDWordValue(HKLM, key, 'Install', install);
    end;
    // .NET 4.0 uses value Servicing instead of SP
    if Pos('v4', version) = 1 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
    end else begin
        success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
    end;
    result := success and (install = 1) and (serviceCount >= service);
end;

function InitializeSetup(): Boolean;
begin
    if not IsDotNetDetected('v4\Client', 0) then begin
        MsgBox('ChessV requires the Microsoft .NET Framework 4.0 Client Profile.'#13#13
            'Please use Windows Update to install this version,'#13
            'and then re-run the ChessV setup program.', mbInformation, MB_OK);
        result := false;
    end else
        result := true;
end;
