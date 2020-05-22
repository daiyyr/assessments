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
        public static string connStrSms = "dsn=manaaki383;DATABASE=manaaki383;UID=root;PASSWORD=onlyoffice;default command timeout=999;";
        static void Main(string[] args)
        {
            string binPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string xmlPath = Path.Combine(
                Directory.GetParent(binPath).Parent.Parent.Parent.Parent.FullName,
                "dbxml");
            OdbcConnection connSms = new OdbcConnection(connStrSms);
            connSms.Open();

            /*
            OdbcCommand commSms = new OdbcCommand(sql, connSms);
            OdbcDataReader dbrd = commSms.ExecuteReader();
            OdbcDataAdapter da1 = new OdbcDataAdapter(summarydt, connSms);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "logdt");
            DataTable logdt = ds1.Tables["logdt"];
            */

            Console.WriteLine("Start to load");


            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(xmlPath, "Items.xml"));
            bool firstItem = true;
            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                firstItem = false;
                if (firstItem)
                {
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
                    createsql += "DisplayTitle )";
                    try
                    {
                        OdbcCommand commSms = new OdbcCommand(createsql, connSms);
                        commSms.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("failed to create table Item: " + ex.Message);
                    }
                }
                string displayTitleS = item.InnerText;

                //insert data
                //to be continued
                foreach(XmlAttribute a in item.Attributes)
                {
                    var c = a.Name;
                    var e = a.Value;
                }
                foreach(XmlNode displayTitle in item.ChildNodes)
                {
                    if(displayTitle.Name == "DisplayTitleUnformatted")
                    {
                        displayTitleS = displayTitle.InnerText;
                    }
                }
            }


            DirectoryInfo d = new DirectoryInfo(xmlPath);
            FileInfo[] Files = d.GetFiles("*.xml");
            foreach (FileInfo file in Files)
            {
                if (
                    file.FullName.Contains("NoteForms.xml")
                    ||
                    file.FullName.Contains("Items.xml")
                    )
                    continue;
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
                                    Console.WriteLine("failed to create table " + dt.TableName + ": " + ex.Message);
                                }
                            }
                        }
                        string insertsql = "insert into " + dt.TableName + " values(";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            insertsql += "'" + dr[dc.ColumnName].ToString().Replace("'","\\'").Replace("\\","\\\\") + "',";
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
                            Console.WriteLine("failed to insert data to table " + dt.TableName + ": " + ex.Message);
                        }
                    }
                }
            }



            Console.WriteLine("XML to SQL succeed.");
        }
    }
}
