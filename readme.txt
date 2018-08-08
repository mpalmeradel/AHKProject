NOTE: This was a project to create an AHK automation script to automate Skype For Business for lecturers at Adelaide TAFE.
It was to provide a simple means of automatically recording a session immediately on log in. It is accompanied with a
setup utility (a WPF project) and an installer created using Inno Setup.

below is an excerpt from an included readme file. My email has been omitted to avoid nuisance emails and internet stalkers.

----------

Attention future coders...

Here are all the source files for this project. If you want to modify them, it is recommended that you place the files in c:\autoskypetest as this is the directory the Inno Setup script refers to.

---------------------
Ideas for Improvement
---------------------

There is much room for improvement.

Firstly, the icon is terrible. I apologise - I was rushed. If you do anything else to this project, please change it for me!

Secondly, much (if not all) of the automation could have been achieved using the Lync API (Lync is the original name for Skype for Business). The process would be much neater if moved out of AutoHotKey and much extra functionality could be added.

Thirdly, I have placed in the config file a place for lecturer name. As Skype For Business automatically names the saved meetings with user name and date, I never used it. However, I kept it as it could be useful in future versions of AutoSkype. One use is to rename the newest recorded file to another username if the Windows username doesn't match that of the lecturers. This would require some mild modifications to CopyLatestFile.bat.

Lastly, there is a problem which may or may not be soluable. Skype For Business takes time to save the recorded meeting into it's folder. To copy the latest file to a directory requires a lengthy wait on the part of the user. This is an unpleasant side effect that I have not figured out a way to circumvent other than to change the settings of Skype For Business to save the file elsewhere on the network. My solution is not ideal - however a clever programmer might find a way around it using the Lync API. If you do find a way, send me a mail telling me how you did it.

--------------------------------
Description of Files/Directories
--------------------------------

AutoSkype.ahk - Contains the automation script in AutoHotKey language. To use and modify, AutoHotKey needs to be installed on the computer.

CopyLatestFile.bat - A simple batch file which copies the latest file placed in a directory and copies to another. It is executed from AutoSkype.ahk and must be in the same directory. It does not accept command line arguments. Instead, details are inputted during execution. This was done as I had a keyboard mapping problem - when I used AutoHotKey to use the " character, I instead recieved the @ character. Other commonly used characters gave no output at all! To add to the problem, different computers gave different characters. I could only use text minus symbols hence the need use batch file inputs opposed to command line arguments (which would have required the " character to contain spaces in names). I hope this all makes sense :)

AutoSkype Setup Utility - contains source (and executable) for AutoSkype Setup Utility. This is a simple setup utility allowing the user to set and alter directories and other information associated with AutoSkype config files. Utilises/creates an initconfig.txt file under {user}/appdata/roaming/autoskype.

Documentation - contains a PDF with simple a simple explanation of program function along with instructions. Is installed with AutoSkype.

Inno Setup Script - contains 'setup file script.iss' which is used in Inno Setup to create an installer. I highly recommend Inno Setup as it is both extremely easy to use and capable.

autoskypeicon.ico - possibly the worst icon ever associated with an application. Please change. Please.

readme.txt - this file.
