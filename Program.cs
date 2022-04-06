using System.Diagnostics;

string _pathToFolder;
Dictionary<string, string> _targetFileCollection;
string _targetFileType;
bool loadingActive = false;

//================ MAIN()
// See https://aka.ms/new-console-template for more information <---this is weird but okay


while (true) {
    do {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Enter Folder Path: ");
        _pathToFolder = Console.ReadLine();
        if(!Directory.Exists(_pathToFolder)) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Path doesn't not exist...");
            Thread.Sleep(750);
            _pathToFolder = String.Empty; 
            Console.Clear();
        }
    }
    while (_pathToFolder == string.Empty);

    do {
        Console.Write("File Type: ");
        _targetFileType = Console.ReadLine();
    }
    while (_targetFileType == string.Empty);

    _targetFileCollection = new Dictionary<string, string>();
    Search(_pathToFolder);
    ReadCollection();

    Console.WriteLine("\n\n\t Q - To Exit the Program | R - to Restart | Enter File name to open location");
    while (true) {
        Console.ForegroundColor = ConsoleColor.White;
        string input = Console.ReadLine();
        if (input == "q" || input == "Q") {
            Environment.Exit(0);
        }
        else if (input == "R" || input == "r") {
            break;
        }
        else {
            OpenFileLocation(input);
            continue;
        }
        //Console.SetCursorPosition(0, Console.CursorTop - 2);
        //ClearLine();
    }
    Console.Clear();
    Thread.Sleep(100);
}


void Search(string filePath) {

    string[] files;
    try {
        if (Directory.Exists(filePath)) {
            files = Directory.GetFiles(filePath);
            var folders = Directory.GetDirectories(filePath);
            loadingActive = true;
            foreach (string fileName in files) {
                if (fileName.Contains(_targetFileType)) {
                    _targetFileCollection.Add(fileName, filePath);
                }
            }

            foreach (string folder in folders) {
                Search(folder);
            }
        }
    }
    catch (UnauthorizedAccessException) { return; }
}


void ReadCollection() {
    if (_targetFileCollection == null || _targetFileCollection.Count <= 0) {
        Console.ForegroundColor = ConsoleColor.Red; 
        Console.WriteLine($"No {_targetFileType} files found");
    }
        
    else {
        Console.WriteLine("{0,5} \t\t {1,5}", "File Name", "File Location");
        Console.WriteLine("------------------------------------");
        foreach (var file in _targetFileCollection) {
            Console.WriteLine("{0,-20} \t\t {1,5}", $"{file.Key.Substring(file.Value.Length + 1)}", $"{file.Value}");
        }
    }
}

void OpenFileLocation(string fileLocation) {
    bool found = false;
    foreach (var file in _targetFileCollection) {
        if (file.Key.Substring(file.Value.Length + 1) == fileLocation) {
            Console.WriteLine($"Opening....");
            Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", file.Value);
            found = true;
        }
    }
    if (!found) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("File path doesn't exist, try again");
    }
}

//======== Console Helpers

//void ClearLine() {
//    var currentLine = Console.CursorTop;
//    Console.SetCursorPosition(0,Console.CursorTop);
//    Console.Write(new string(' ', Console.WindowWidth));
//    Console.SetCursorPosition(0, currentLine);
//}
//void ShowLoading() {
//    var counter = 0;
//    while (true) {
//        switch (counter % 5) {
//            case 0:
//                Console.Write("Loading [#****]");
//                break;
//            case 1:
//                Console.Write("Loading [##***]");
//                break;
//            case 2:
//                Console.Write("Loading [###**]");
//                break;
//            case 3:
//                Console.Write("Loading [####*]");
//                break;
//            case 4:
//                Console.Write("Loading [#####]");
//                break;
//        }
//        Console.SetCursorPosition(Console.CursorLeft - 15, Console.CursorTop);
//        counter++;
//        Thread.Sleep(500); 
//    }
//}


