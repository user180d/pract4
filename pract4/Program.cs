using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;
using McMaster.Extensions.CommandLineUtils;
using pract4;

class Program
{
    MasterLab lab;
    string LAB_PATH;
    string inputFile, outputFile;
    static void Main(string[] args)
        => CommandLineApplication.Execute<Program>(args);
    void printlab() 
    {
        Console.WriteLine("Enter -i or --input + path, -o or --output + path if needed\n Menu: \nlab1 - process lab 1 \nlab2 - process lab 2 \nlab3 - process lab 3 \nexit - close app");
    }
    void saveIandO(string[] t)
    {
        int i = 0;  
        if (t[1] == "-i" || t[1] == "--input")
        {
            inputFile = t[2];
            i++;
        }
        if (t[3] == "-o" || t[3] == "--output")
        {
            outputFile = t[4];
            i++;
        }

    }
    string  findType(int l)
    {
        for (int i = 0; i < l; i++)
        {
            if (LAB_PATH[i] == '/' )
            {
                return "/";
            }
            if (LAB_PATH[i].ToString() == @"\")
            {
                return @"\";
            }
        }
        return "";
    }
    void checkFormat() 
    {
        int l = LAB_PATH.Length;
        if (LAB_PATH[l-1] != '/' || LAB_PATH[l-1].ToString() != @"\")
        {
            LAB_PATH += findType(l);
        }
    }
    void useDirPath()
    {
        checkFormat();
        inputFile= LAB_PATH+"INPUT.txt";
        outputFile = LAB_PATH +"OUTPUT.txt";
    }
    bool checkHomeDir()
    {
        LAB_PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        useDirPath();
        if (File.Exists(inputFile))
        {
            return true;
        }
        return false;
    }
    
    void process(string str)
    {
        bool operSuccess = true;
        if (inputFile == null) 
        {
            if (LAB_PATH != null)
            {
                useDirPath();
            }
            else 
            {
                if (checkHomeDir()==false) {
                    str = "empty";
                }
            }
        }
        switch (str)
        {
            case "empty": Console.WriteLine("No file to work with"); break;
            case "exit":
                break;
            case "lab1":
                Console.WriteLine("=============== LAB_1 ===============");
                lab = new Lab1(inputFile,outputFile);
                operSuccess = lab.start();
                if (operSuccess != false)
                {
                    Console.Write("Result - Success");
                    break;
                }
                Console.Write("Result - Program crusshed returning to the main menu");
                break;

            case "lab2":
                Console.WriteLine("=============== LAB_2 ===============");
                lab = new Lab2(inputFile, outputFile);
                operSuccess = lab.start();
                if (operSuccess != false)
                {
                    Console.Write("Result - Success");
                    break;
                }
                Console.Write("Result - Program crusshed returning to the main menu");
                break;

            case "lab3":
                Console.WriteLine("=============== LAB_3 ===============");
                lab = new Lab3(inputFile, outputFile);
                operSuccess = lab.start();
                if (operSuccess != false)
                {
                    Console.Write("Result - Success");
                    break;
                }
                Console.Write("Result - Program crusshed returning to the main menu");
                break;
                
        }
    }
    void saveDir(string[] t)
    {
        if (t[1] == "-p" || t[1]=="--path")
        {
            LAB_PATH = t[2];
        }
    }
    void OnExecute()
    {
        string[] token=null;
        bool correcctInput=false;
        string menu = "";
        while (menu != "exit")
        {
            
            Console.WriteLine("\nPlease enter: \n    'run' to start\n    'set-path -p' - path to a with input and output files, -p is a path \n    'exit' to close app\n    'version' - additional info ");
            while (!correcctInput) {
                menu = Console.ReadLine();
                token = menu.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (token[0] == "exit" || token[0] == "version" || token[0] == "run" || (token[0] == "set-path" && token.Length==3))
                { 
                    correcctInput= true;
                }
            }
            correcctInput  = false;
            switch (token[0])
            {
                
                case "exit": break;
                case "version": Console.WriteLine("Author - D Taranov\nVersion: 1.0.0\n"); break;
                case "set-path":
                    if (token.Length == 3)
                    {
                        saveDir(token);
                    }
                    break;
                case "run":
                    printlab();
                    menu = Console.ReadLine();
                    token = menu.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (token.Length == 5)
                    {
                        saveIandO(token);
                    }
                    process(token[0]);
                    break;
            }
        }
    }
}