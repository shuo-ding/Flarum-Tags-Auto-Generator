# Flarum-Tags-Auto-Generator
Author Dr Shuo Ding 2021

This program generates MySQL scripts to insert the tags and sub-tags into the Tags table in your Flarum database.
It has been tested working in Flarum 1.04

Please first delete all entries (BACKUP FIRST, as your old tags will be deleted totally) in your Tags table before paste the script in your mysql console

I include a WindowsApp.zip for you to run it directly from your Windows PC.
You can modify the config.txt file to be the format you like. 
Please note the "end" string is used to split rows and it has to be there.

You need to input 5 parameters for each row: Name, Slug, Desc, Color, and Icon, which are seperated by "/". 

The first row following each "end" is the Top level tag, and the rests are sub-tags.
The last "end" must be remained there in the end of the txt file.

Open the "myscript.txt" after running the program, and paste it to the mysql console.
Once you see "Query OK, 1 row affected (0.00 sec)" then it is all successful. 

After you paste all scripts, you can Ctrl +F5 to refresh your Flarum pages to see your new tags.

Advantages- back up and restore hundreds of tags in seconds
          - more comfortable to operate in TXT config and just wait for results to happen
          



