/* Copyright (c) 2021 Shuo Ding
  12 July 2021
  Flarum 1.04 Categories Generators   
  Author Dr Shuo Ding, 2021  
  Website: https://www.IoTNextDay.com  
  Email: Shuo.Ding.Australia@Gmail.com  
  All Right Reserved 
  Distribution of the copy of this software must include this copyright information   
*/

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Collections.Generic;

/*mysql> +---------------------------+--------------+------+-----+---------+----------------+
| Field                     | Type         | Null | Key | Default | Extra          |
+---------------------------+--------------+------+-----+---------+----------------+
| id                        | int unsigned | NO   | PRI | NULL    | auto_increment |
| name                      | varchar(100) | NO   |     | NULL    |                |
| slug                      | varchar(100) | NO   | UNI | NULL    |                |
| description               | text         | YES  |     | NULL    |                |
| color                     | varchar(50)  | YES  |     | NULL    |                |
| background_path           | varchar(100) | YES  |     | NULL    |                |
| background_mode           | varchar(100) | YES  |     | NULL    |                |
| position                  | int          | YES  |     | NULL    |                |
| parent_id                 | int unsigned | YES  | MUL | NULL    |                |
| default_sort              | varchar(50)  | YES  |     | NULL    |                |
| is_restricted             | tinyint(1)   | NO   |     | 0       |                |
| is_hidden                 | tinyint(1)   | NO   |     | 0       |                |
| discussion_count          | int unsigned | NO   |     | 0       |                |
| last_posted_at            | datetime     | YES  |     | NULL    |                |
| last_posted_discussion_id | int unsigned | YES  | MUL | NULL    |                |
| last_posted_user_id       | int unsigned | YES  | MUL | NULL    |                |
| icon                      | varchar(100) | YES  |     | NULL    |                |
+---------------------------+--------------+------+-----+---------+----------------+

*/
public class LogFile
{
    public StreamWriter sw;
    public void Close()
    {
        sw.Close();
    }
    public void Flush()
    {
        sw.Flush();
    }
    public LogFile(string name)
    {
        sw = new StreamWriter(name);
    }
    public void Write(string str)
    {
        //  Console.WriteLine(str);
        sw.WriteLine(str);
    }
}
public class Topics
{
    public int id { get; set; } //auto 
    public string name { get; set; } //customise 
    public string slug { get; set; } //customise 
    public string description { get; set; } //customise 
    public string color { get; set; } //customise 
    public string background_path { get; set; }
    public string background_mode { get; set; }
    public int position { get; set; }//auto
    public string parent_id { get; set; }//auto 
    public string default_sort { get; set; }
    public string is_restricted { get; set; }
    public string is_hidden { get; set; }
    public string discussion_count { get; set; }
    public string last_posted_at { get; set; }
    public string last_posted_discussion_id { get; set; }
    public string last_posted_user_id { get; set; }
    public string icon { get; set; } //customise 

    public string mysqlstring = "insert into tags (id,name,slug,description,color,background_path,background_mode,position,parent_id,default_sort,is_restricted,is_hidden,discussion_count,last_posted_at,last_posted_discussion_id,last_posted_user_id,icon) values ('myid','myname','myslug','mydescription','mycolor','mybackground_path','mybackground_mode','myposition',myparent_id,'mydefault_sort','myis_restricted','myis_hidden','mydiscussion_count',current_time(),mylast_posted_discussion_id,mylast_posted_user_id,'myicon');";

    public string GetSQLstring(string configTxt, int thisid, int thispos, int parent)
    {
        //clear content
        id = -1;name = ""; slug = ""; description = ""; color = ""; background_path = ""; background_mode = ""; position = -1; default_sort = ""; is_restricted = ""; is_hidden = ""; discussion_count = ""; last_posted_at = ""; last_posted_discussion_id = ""; last_posted_user_id = ""; icon = "";        
        //Notice the 3 fields are MUL key, so by default we have to set them to NULL, as we do not have valid foreign keys
        parent_id = "NULL";
        last_posted_user_id = "NULL";
        last_posted_discussion_id = "NULL";

        string res = mysqlstring;
        default_sort = "";
        is_restricted = "0";
        is_hidden = "0";

        discussion_count = "1";
        background_path = "";
        background_mode = "";
        string[] splitstring = configTxt.Split('/');
        int len = splitstring.Length;
        if ((len != 5) && (configTxt != "end"))
        {
            string warn = "Error! There MUST be 5 parameters in your config entry: Please reconfig ! " + splitstring;
            Console.WriteLine(warn);
            return warn;
        }
        id = thisid;
        position = thispos;
        if (parent != -1)
            parent_id = parent.ToString();

        name = splitstring[0];
        slug = splitstring[1];
        description = splitstring[2];
        color = splitstring[3];
        icon = splitstring[4];
        string sqlstr = res.Replace("myid", id.ToString()).Replace("myname", name).Replace("myslug", slug).Replace("mydescription", description).Replace("mycolor", color).Replace("mybackground_path", background_path).Replace("mybackground_mode", background_mode).Replace("myposition", position.ToString()).Replace("myparent_id", parent_id).Replace("mydefault_sort", default_sort).Replace("myis_restricted", is_restricted).Replace("myis_hidden", is_hidden).Replace("mydiscussion_count", discussion_count).Replace("mylast_posted_at", last_posted_at).Replace("mylast_posted_discussion_id", last_posted_discussion_id).Replace("mylast_posted_user_id", last_posted_user_id).Replace("myicon", icon);
        return sqlstr;
    }
    public string Show(string configTxt, int thisid, int thispos, int parent, LogFile log)
    {
        string sql = GetSQLstring(configTxt, thisid, thispos, parent);
        string warn = "---------------   " + name + " id " + id + " Slug " + slug + " parent " + parent_id + "  position " + position;
        Console.WriteLine(warn);
        log.Write(sql);
        log.Flush();
        return sql;
    }
}

class LevelOneTopics : Topics
{
    public List<Topics> subtopiclist;
    public LevelOneTopics()
    {
        subtopiclist = new List<Topics>();
    }
}

class Program
{
    static void Main(string[] args)
    {
        LogFile log = new LogFile("mysqlscript.txt");
        int counter = 0;
        string line;
        List<string> mylist = new List<string>();
        // Read the file and display it line by line.  
        System.IO.StreamReader file =
            new System.IO.StreamReader("config.txt");
        while ((line = file.ReadLine()) != null)
        {
            string newline = line.Trim();

            if (newline.Length != 0)
            {
                mylist.Add(newline);
                Console.WriteLine(newline);
            }
            counter++;
        }
        file.Close();
        System.Console.WriteLine("There were {0} lines.", counter);
        // Suspend the screen.
        // 
        List<LevelOneTopics> topiclist = new List<LevelOneTopics>();
        int id = 1;
        int position = 0;

        for (int i = 0; i < mylist.Count; i++)
        {
            if (mylist[i] == "end")
            {
                if (i == mylist.Count - 1)
                    break;
                i++;
                LevelOneTopics newtopic = new LevelOneTopics();
                newtopic.Show(mylist[i], id, position, -1, log); //parent is 0 or NULL  
                position++;
                id++;
                int subposition = 0;
                topiclist.Add(newtopic);
                int j = i + 1;
                while (mylist[j] != "end" && j < mylist.Count - 1)
                {
                    Topics subtopic = new Topics();
                    subtopic.Show(mylist[j], id, subposition, newtopic.id, log);
                    subposition++;
                    id++;
                    newtopic.subtopiclist.Add(subtopic);
                    j++;
                }
            }
        }
        log.Flush();
        log.Close();
    }
}


