# Flarum-Tags-Auto-Generator
                            Author Dr Shuo Ding 2021

This program generates MySQL scripts to insert the tags and sub-tags into the Tags table in your Flarum database.
It has been tested working in Flarum 1.04

Please first delete all entries (BACKUP FIRST, AS ALL TAGS WILL BE REMOVED) in your Tags table before pasting the script in your mysql console. You can do this by connecting to mysql console, selecting to use your own database, and inputting "delete from tags;"

I include a WindowsApp.zip for you to run it directly from your Windows PC.
You can also download my code and build it in Visual Studio 2019.

You can modify the config.txt file to be the perfect format you like. 
The config.txt example is:

"
end
Forum/forum/News, board management, release/#009900/1
Management/manage/Board issue management/#ffcc99/1
Complaint/complaint/All about complaints/#ffcc99/1
Registration/reg/Helps on registrations/#009900/1
Cancel/cancel/Cancel registrations/#009900/1
end
Shopping/shopping/Shopping heavens for you/#33cc33/1
Home/home/Furniture, electrics, garden, appliances/#33cc33/1
Computers/computer/Computers, printers, laptops, surface, and monitors/#33cc33/1
Vouchers/voucher/Discount vouchers, specials/#993399/1
Clothes/cloth/Clothes, cases, chests, jewelry, accessories/#993399/1
end
Cars/cars/New cars, second hand cars, boats/#009900/1
Sedans/sedan/All model sedans /#009900/1
SUV/suv/All about SUVs/#009900/1
Trucks/truck/Commercial trucks, UTEs, VANs/#009900/1
Boats/boat/All abot boats/#009900/1
end
"

Please note the "end" string is used to seperate rows and it has to be there.
You need to input 5 parameters for each row: Name, Slug, Desc, Color, and Icon, which are seperated by "/". 
The first row following each "end" is the Top level tag, and the rests are sub-tags.
The last "end" must be remained there in the end of the txt file.

Open the "myscript.txt" after running the program, and paste it to the mysql console.
Once you see "Query OK, 1 row affected (0.00 sec)" then it is all successful. 

Then you can Ctrl +F5 to refresh your Flarum pages to see your new tags.

Advantages  - Restore hundreds of tags in seconds
            - More comfortable to operate in TXT config and just wait for results to happen automatically 
            



Welcome to visit my research website. 

<a href="https://iotnextday.com" style="font-size:19px">IoT Next Day</a>
 
 

          




