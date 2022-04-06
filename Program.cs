using System.Diagnostics;

string _pathToFolder;
Dictionary<string, string> _targetFileCollection;
string _targetFileType;

//================ MAIN()
// See https://aka.ms/new-console-template for more information <---this is weird but okay

do {
    Console.Write("Enter Folder Path: ");
    _pathToFolder = Console.ReadLine();
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

//open files folder
//Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", _pathToFolder);



void Search(string path) {

    var files = Directory.GetFiles(path);
    var folders = Directory.GetDirectories(path);

    foreach (string file in files) {
        if (file.Contains(_targetFileType)) {
            AddToCollection(file, path);
        }

    }

    foreach (string folder in folders) {
        Search(folder);
    }
}

void AddToCollection(string file, string location) {
    _targetFileCollection.Add(location, file);
}

void ReadCollection() {
    if (_targetFileCollection == null || _targetFileCollection.Count <= 0)
        Console.WriteLine($"No {_targetFileType} files found");
    foreach (var file in _targetFileCollection) {
    
        Console.WriteLine($"{file.Value.Substring(file.Key.Length + 1)} || Location: {file.Key} ");
    }

}
