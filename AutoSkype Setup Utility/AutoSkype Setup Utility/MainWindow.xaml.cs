using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

//using System.Windows.Forms;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoSkype_Setup_Utility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // NOTE: Solo Learn

        // class attributes
        private string autoSkypeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AutoSkype\";
        private string sharedConfigDirectory;
        private string initConfig = @"initconfig.txt";
        private string sharedConfig = @"config.txt";
        
        public MainWindow()
        {
            InitializeComponent();

            bool didNotQuit = true;
            string filePath = autoSkypeDirectory + initConfig;
            // check if initconfig.txt exists

            Directory.CreateDirectory(autoSkypeDirectory);
    
                bool doesExist = System.IO.File.Exists(filePath);
                if (!doesExist) 
                {
                    MessageBoxResult result = MessageBox.Show(
                        "There is no initconfig file in roaming directory\nUsing [" +
                        filePath + "]\nWould you like to create it?",
                        "Create initconfig.txt", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // create new file
                        File.WriteAllText(filePath, "\n\n");


                        
                    } else
                    {
                        MessageBox.Show("This program will now quit.");
                        didNotQuit = false;
                        this.Close();
                    }
                }


            if (didNotQuit)
            {
                // load initConfig and place into fields
                try
                {
                    // Open text file using stream reader
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        // Read stream to string
                        sharedConfigDirectoryTextBox.Text = sr.ReadLine();
                        fileSaveLocationsTextBox.Text = sr.ReadLine();
                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show("The initconfig file could not be read.\n" + e.ToString());
                    this.Close();
                }

                // enable first two fields and button
                sharedConfigDirectoryTextBox.IsEnabled = true;
                fileSaveLocationsTextBox.IsEnabled = true;

                initConfigButton.IsEnabled = true;

                sharedDirectoryBrowseButton.IsEnabled = true;
                videoSaveBrowseButton.IsEnabled = true;

                // attempt to load config.txt from shared config directory
                string sharedDirectory = sharedConfigDirectoryTextBox.Text;
                string sharedFilePath = "";

                if (sharedDirectory != "")
                    sharedFilePath = sharedDirectory + sharedConfig;

                if (System.IO.File.Exists(sharedFilePath))
                {
                    // load config.txt
                    try
                    {
                        // Open text file using stream reader
                        using (StreamReader sr = new StreamReader(sharedFilePath))
                        {
                            // Read stream to string

                            meetingURLTextBox.Text = sr.ReadLine();
                            lecturerNameTextBox.Text = sr.ReadLine();
                            finalFileLocationTextBox.Text = sr.ReadLine();


                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("The config file could not be read.\n" + e.ToString());
                    }

                    // enable relevant text boxes and buttons
                    meetingURLTextBox.IsEnabled = true;
                    lecturerNameTextBox.IsEnabled = true;
                    finalFileLocationTextBox.IsEnabled = true;
                    sharedConfigButton.IsEnabled = true;
                    finalFileBrowseButton.IsEnabled = true;
                }
            }   
       }

        
        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to quit?", "Quit", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                this.Close();
        }

        private void initConfigButton_Click(object sender, RoutedEventArgs e)
        {
            // create file path string
            string filePath = autoSkypeDirectory + initConfig;

            // write initconfig stuff to file
            string sharedDirectory = this.addBackSlash(sharedConfigDirectoryTextBox.Text);
            string fileSaveLocations = this.addBackSlash(fileSaveLocationsTextBox.Text);

            // if backslashes were added, useful to update screen
            sharedConfigDirectoryTextBox.Text = sharedDirectory;
            fileSaveLocationsTextBox.Text = fileSaveLocations;

            File.WriteAllText(filePath, sharedDirectory + "\n" + fileSaveLocations + "\n");

            MessageBox.Show("Contents have been saved to " + initConfig);

            // if config.txt doesn't exist, create
            string sharedFilePath = sharedDirectory + sharedConfig;
            if(!System.IO.File.Exists(sharedFilePath))
            {
                File.WriteAllText(sharedFilePath, "\n\n\n");
            } else
            {
                // load config.txt
                try
                {
                    // Open text file using stream reader
                    using (StreamReader sr = new StreamReader(sharedFilePath))
                    {
                        // Read stream to string

                        meetingURLTextBox.Text = sr.ReadLine();
                        lecturerNameTextBox.Text = sr.ReadLine();
                        finalFileLocationTextBox.Text = sr.ReadLine();
                        
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The config file could not be read.\n" + ex.ToString());
                }
            }

            // enable relevant text boxes and buttons
            meetingURLTextBox.IsEnabled = true;
            lecturerNameTextBox.IsEnabled = true;
            finalFileLocationTextBox.IsEnabled = true;
            sharedConfigButton.IsEnabled = true;
            finalFileBrowseButton.IsEnabled = true;

        }

        private void sharedConfigButton_Click(object sender, RoutedEventArgs e)
        {
            // create file path string
            string sharedFilePath = sharedConfigDirectoryTextBox.Text + sharedConfig;

            // write config stuff to file
            string meetingURL = meetingURLTextBox.Text;
            string lecturerName = lecturerNameTextBox.Text;
            string finalFileLocation = this.addBackSlash(finalFileLocationTextBox.Text);

            // make sure fields end with backslash
            finalFileLocationTextBox.Text = finalFileLocation;

            File.WriteAllText(sharedFilePath,
                meetingURL + "\n" +
                lecturerName + "\n" +
                finalFileLocation + "\n");

            MessageBox.Show("Contents have been saved to " + sharedConfig);
        }

        private void sharedDirectoryBrowseButton_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = sharedConfigDirectoryTextBox.Text;
            if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sharedConfigDirectoryTextBox.Text = fbd.SelectedPath + @"\";
            }
        }

        private void videoSaveBrowseButton_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = fileSaveLocationsTextBox.Text;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileSaveLocationsTextBox.Text = fbd.SelectedPath + @"\";
            }
        }

        private void finalFileBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = finalFileLocationTextBox.Text;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                finalFileLocationTextBox.Text = fbd.SelectedPath + @"\";
            }
        }

        // make sure last character is a backslach
        private string addBackSlash(string s)
        {
            char last = s[s.Length - 1];
            
            if (last != '\\' )
                s = s + @"\";
            return s;
        }
        
    }
}
