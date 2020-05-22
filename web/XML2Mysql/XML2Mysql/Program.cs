using System;
using System.Reflection;
using System.IO;
using System.Data.Odbc;
using System.Data;
using System.Xml;

namespace XML2Mysql
{
    class Program
    {

        //before import:
        //ALTER DATABASE manaaki383 CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;


        public static string connStrSms = "dsn=manaaki383;DATABASE=manaaki383;UID=root;PASSWORD=onlyoffice;default command timeout=999;";
        static void Main(string[] args)
        {
            string binPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string xmlPath = Path.Combine(
                Directory.GetParent(binPath).Parent.Parent.Parent.Parent.FullName,
                "dbxml");
            OdbcConnection connSms = new OdbcConnection(connStrSms);
            connSms.Open();

            XmlDocument doc = new XmlDocument();
            bool firstItem = true;
            Console.WriteLine("Start to load");
            
            #region Items.xml
            doc.Load(Path.Combine(xmlPath, "Items.xml"));
            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                if (firstItem)
                {
                    firstItem = false;
                    string createsql = "create table Item (";
                    bool firstColumn = true;
                    foreach (XmlAttribute a in item.Attributes)
                    {
                        if (firstColumn)
                        {
                            firstColumn = false;
                            createsql += a.Name + " varchar(100) primary key,";
                        }
                        else
                        {
                            createsql += a.Name + " varchar(100),";
                        }
                    }
                    createsql += "DisplayTitle longtext )";
                    try
                    {
                        OdbcCommand commSms = new OdbcCommand(createsql, connSms);
                        commSms.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Processing file Items.xml, failed to create table Item: " + ex.Message);
                    }
                }

                string insertsql = "insert into Item set ";
                foreach (XmlAttribute a in item.Attributes)
                {
                    insertsql += a.Name + "='" + a.Value.Replace("\\", "\\\\").Replace("'", "\\'") + "',";
                }
                string displayTitleS = item.InnerText;
                foreach (XmlNode displayTitle in item.ChildNodes)
                {
                    if(displayTitle.Name == "DisplayTitleUnformatted")
                    {
                        displayTitleS = displayTitle.InnerText;
                        break;
                    }
                }
                insertsql += "DisplayTitle='" + displayTitleS.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
                try
                {
                    OdbcCommand commSms2 = new OdbcCommand(insertsql, connSms);
                    commSms2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Processing file Items.xml, failed to insert data to table " + "Item" + ": " + ex.Message);
                }
            }
            
            #endregion
            #region NoteForms.xml
            doc = new XmlDocument();
            doc.Load(Path.Combine(xmlPath, "NoteForms.xml"));
            firstItem = true;
            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                if (firstItem)
                {
                    firstItem = false;
                    string createsql = "create table NoteForm (";
                    bool firstColumn = true;
                    foreach (XmlAttribute a in item.Attributes)
                    {
                        if (firstColumn)
                        {
                            firstColumn = false;
                            createsql += a.Name + " varchar(100) primary key,";
                        }
                        else
                        {
                            createsql += a.Name + " varchar(100),";
                        }
                    }
                    createsql += "Content longtext )";
                    try
                    {
                        OdbcCommand commSms = new OdbcCommand(createsql, connSms);
                        commSms.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Processing file NoteForms.xml, failed to create table NoteForm: " + ex.Message);
                    }
                }

                string insertsql = "insert into NoteForm set ";
                foreach (XmlAttribute a in item.Attributes)
                {
                    insertsql += a.Name + "='" + a.Value.Replace("\\", "\\\\").Replace("'", "\\'") + "',";
                }
                string displayTitleS = item.InnerText;
                foreach (XmlNode displayTitle in item.ChildNodes)
                {
                    if (displayTitle.Name == "DisplayTitleUnformatted")
                    {
                        displayTitleS = displayTitle.InnerText;
                        break;
                    }
                }
                insertsql += "Content='" + displayTitleS.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
                try
                {
                    OdbcCommand commSms2 = new OdbcCommand(insertsql, connSms);
                    commSms2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Processing file NoteForms.xml, failed to insert data to table " + "NoteForm" + ": " + ex.Message);
                }
            }
            #endregion
            

            #region NoteFormNoteImages.xml
            doc = new XmlDocument();
            doc.Load(Path.Combine(xmlPath, "NoteFormNoteImages.xml"));
            firstItem = true;
            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                if (firstItem)
                {
                    firstItem = false;
                    string createsql = "create table NoteFormNoteImage (";
                    bool firstColumn = true;
                    foreach (XmlAttribute a in item.Attributes)
                    {
                        if (firstColumn)
                        {
                            firstColumn = false;
                            createsql += a.Name + " varchar(100) primary key,";
                        }
                        else
                        {
                            createsql += a.Name + " varchar(100),";
                        }
                    }
                    createsql += "Caption longtext )";
                    try
                    {
                        OdbcCommand commSms = new OdbcCommand(createsql, connSms);
                        commSms.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Processing file NoteFormNoteImages.xml, failed to create table NoteFormNoteImage: " + ex.Message);
                    }
                }

                string insertsql = "insert into NoteFormNoteImage set ";
                foreach (XmlAttribute a in item.Attributes)
                {
                    insertsql += a.Name + "='" + a.Value.Replace("\\", "\\\\").Replace("'", "\\'") + "',";
                }
                string displayTitleS = item.InnerText;
                foreach (XmlNode displayTitle in item.ChildNodes)
                {
                    if (displayTitle.Name == "Caption")
                    {
                        displayTitleS = displayTitle.InnerText;
                        break;
                    }
                }
                insertsql += "Caption='" + displayTitleS.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
                try
                {
                    OdbcCommand commSms2 = new OdbcCommand(insertsql, connSms);
                    commSms2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Processing file NoteFormNoteImages.xml, failed to insert data to table " + "NoteFormNoteImage" + ": " + ex.Message);
                }
            }
            #endregion

            #region other tables
            DirectoryInfo d = new DirectoryInfo(xmlPath);
            FileInfo[] Files = d.GetFiles("*.xml");
            foreach (FileInfo file in Files)
            {
                if (
                    file.FullName.Contains("NoteForms.xml")
                    ||
                    file.FullName.Contains("Items.xml")
                    ||
                    file.FullName.Contains("NoteFormNoteImages.xml")
                    )
                {
                    continue;
                }
                DataSet ds = new DataSet();
                ds.ReadXml(file.FullName);
                foreach(DataTable dt in ds.Tables)
                {
                    bool firstRow = true;
                    foreach(DataRow dr in dt.Rows)
                    {
                        if (firstRow)
                        {
                            firstRow = false;
                            string sql = "select * from "+ dt.TableName;
                            try
                            {
                                OdbcCommand commSms = new OdbcCommand(sql, connSms);
                                OdbcDataReader dbrd = commSms.ExecuteReader();
                            }
                            catch
                            {
                                string createsql = "create table " + dt.TableName + "(";
                                bool firstColumn = true;
                                foreach(DataColumn dc in dt.Columns)
                                {
                                    if (firstColumn)
                                    {
                                        firstColumn = false;
                                        createsql += dc.ColumnName + " varchar(100) primary key,";
                                    }
                                    else
                                    {
                                        createsql += dc.ColumnName + " varchar(100),";
                                    }
                                }
                                createsql = createsql.Remove(createsql.Length - 1);
                                createsql += ")";
                                try
                                {
                                    OdbcCommand commSms = new OdbcCommand(createsql, connSms);
                                    commSms.ExecuteNonQuery();
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine("Processing file "+ file.Name + ", failed to create table " + dt.TableName + ": " + ex.Message);
                                }
                            }
                        }
                        string insertsql = "insert into " + dt.TableName + " values(";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            insertsql += "'" + dr[dc.ColumnName].ToString().Replace("\\", "\\\\").Replace("'","\\'") + "',";
                        }
                        insertsql = insertsql.Remove(insertsql.Length - 1);
                        insertsql += ")";
                        try
                        {
                            OdbcCommand commSms2 = new OdbcCommand(insertsql, connSms);
                            commSms2.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Processing file " + file.Name + ", failed to insert data to table " + dt.TableName + ": " + ex.Message);
                        }
                    }
                }
            }
            #endregion


            Console.WriteLine("XML to SQL succeed.");
        }
    }
}
