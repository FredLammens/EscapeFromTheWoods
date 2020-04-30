using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTheWoods
{
    class Databank
    {
        private string connectionString;
        static int woodRecordID = 1;
        static int monkeyRecordID = 1;
        static int logID = 1;
        public Databank(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public void InsertAll(Bos bos)
        {
            DataSet set = GetDataTables(bos);
            DataTableCollection collection = set.Tables;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlBulkCopy sqlBulk = new SqlBulkCopy(connection))
                {
                    for (int i = 0; i < collection.Count; i++)
                    {
                        DataTable table = collection[i];
                        sqlBulk.BulkCopyTimeout = 0;
                        sqlBulk.DestinationTableName = table.TableName;
                        Task.Run(async () => { await sqlBulk.WriteToServerAsync(table); }).Wait();
                        Console.WriteLine($"{table.TableName} is in database toegevoegd");
                    }
                }
            }
        }
        private static DataSet GetDataTables(Bos bos)
        {
            Console.WriteLine("getting info");
            //WoodRecords
            DataTable woodRecords = new DataTable("WoodRecords");
            woodRecords.Columns.Add("recordId", typeof(int));
            woodRecords.Columns.Add("woodID", typeof(int));
            woodRecords.Columns.Add("treeID", typeof(int));
            woodRecords.Columns.Add("x", typeof(int));
            woodRecords.Columns.Add("y", typeof(int));
            woodRecords.PrimaryKey = new DataColumn[] { woodRecords.Columns["recordId"] };
            //MonkeyRecords
            DataTable monkeyRecords = new DataTable("MonkeyRecords");
            monkeyRecords.Columns.Add("recordID", typeof(int));
            monkeyRecords.Columns.Add("monkeyID", typeof(int));
            monkeyRecords.Columns.Add("monkeyName", typeof(string));
            monkeyRecords.Columns.Add("woodID", typeof(int));
            monkeyRecords.Columns.Add("seqnr", typeof(int));
            monkeyRecords.Columns.Add("treeID", typeof(int));
            monkeyRecords.Columns.Add("x", typeof(int));
            monkeyRecords.Columns.Add("y", typeof(int));
            monkeyRecords.PrimaryKey = new DataColumn[] { monkeyRecords.Columns["recordID"] };
            //Logs
            DataTable logs = new DataTable("Logs");
            logs.Columns.Add("Id", typeof(int));
            logs.Columns.Add("woodID", typeof(int));
            logs.Columns.Add("monkeyID", typeof(int));
            logs.Columns.Add("message", typeof(string));
            logs.PrimaryKey = new DataColumn[] { logs.Columns["Id"] };
            #region datatables aanvullen
            //WoodRecords
            int bosId = bos.id;
            for (int i = 0; i < bos.bomen.Count; i++)
            {
                Boom currentBoom = bos.bomen[i];
                woodRecords.Rows.Add(woodRecordID, bosId, currentBoom.id, currentBoom.xCoordinaat, currentBoom.yCoordinaat);
                woodRecordID++;
            }
            //MonkeyRecords
            for (int i = 0; i < bos.log.ontsnapteApen.Count; i++)
            {
                Aap currentAap = bos.log.ontsnapteApen[i];
                for (int seqnr = 0; seqnr < currentAap.bezochteBomen.Count; seqnr++)
                {
                    Boom currentBoom = currentAap.bezochteBomen[seqnr];
                    monkeyRecords.Rows.Add(monkeyRecordID, currentAap.id, currentAap.naam, bosId, seqnr, currentBoom.id, currentBoom.xCoordinaat, currentBoom.yCoordinaat);
                    monkeyRecordID++;
                    //Logs
                    logs.Rows.Add(logID, bosId, currentAap.id, $"{currentAap.naam} is now in tree {currentBoom.id} at location ({currentBoom.xCoordinaat},{currentBoom.yCoordinaat})");
                    logID++;
                }
            }
            #endregion
            //create DataSet => kan gebruikt worden om xml mee te maken 
            DataSet set = new DataSet("EscapeFromTheWoods");
            set.Tables.Add(woodRecords);
            set.Tables.Add(monkeyRecords);
            set.Tables.Add(logs);
            return set;
        }
        #region testXml
        public static void MakeXMlFile(string path,Bos bos) 
        {
            string xml = GetDataTables(bos).GetXml();
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "EscapeFromTheWoods.xml")))
            {
                sw.Write(xml);
            }
        }
        #endregion
    }
}
