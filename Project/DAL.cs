using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Data
{
    internal class Connect
    {
        private static String cliComConnectionString = GetConnectString();

        internal static String ConnectionString { get => cliComConnectionString; }

        private static String GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "College1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;
        }
    }

    internal class DataTables
    {
        private static SqlDataAdapter adapterProg = InitAdapterProg();
        private static SqlDataAdapter adapterCo = InitAdapterCo();
        private static SqlDataAdapter adapterSt = InitAdapterSt();
        private static SqlDataAdapter adapterEn = InitAdapterEn();
        

        private static DataSet ds = InitDataSet();

        private static SqlDataAdapter InitAdapterProg()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Programs ORDER BY ProgId ",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterCo()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Courses ORDER BY CId ",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterSt()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Students ORDER BY StId ",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterEn()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Enrollments ORDER BY StId, CId ",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }
        

        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();          
            loadProg(ds);          
            loadCo(ds); 
            loadSt(ds);
            loadEn(ds);          
            return ds;
        }

        private static void loadProg(DataSet ds)
        {
            // =========================================================================
            
            // =========================================================================

            adapterProg.Fill(ds, "Programs");

            // =========================================================================
            ds.Tables["Programs"].Columns["ProgId"].AllowDBNull = false;
            ds.Tables["Programs"].Columns["ProgName"].AllowDBNull = false;

            ds.Tables["Programs"].PrimaryKey = new DataColumn[1]
                    { ds.Tables["Programs"].Columns["ProgId"]};
            // =========================================================================
        }

        private static void loadCo(DataSet ds)
        {
            adapterCo.Fill(ds, "Courses");

            // =========================================================================
            ds.Tables["Courses"].Columns["CId"].AllowDBNull = false;
            ds.Tables["Courses"].Columns["CName"].AllowDBNull = false;
            

            ds.Tables["Courses"].PrimaryKey = new DataColumn[1] 
                    { ds.Tables["Courses"].Columns["CId"]};
            // =========================================================================    
        }

        private static void loadSt(DataSet ds)
        {
            adapterSt.Fill(ds, "Students");

            // =========================================================================
            ds.Tables["Students"].Columns["StId"].AllowDBNull = false;
            ds.Tables["Students"].Columns["StName"].AllowDBNull = false;
            

            ds.Tables["Students"].PrimaryKey = new DataColumn[1]
                    { ds.Tables["Students"].Columns["StId"]};
            // =========================================================================    
        }
        private static void loadEn(DataSet ds)
        {
            adapterEn.Fill(ds, "Enrollments");

            // =========================================================================
            ds.Tables["Enrollments"].Columns["CId"].AllowDBNull = false;
            ds.Tables["Enrollments"].Columns["StId"].AllowDBNull = false;

            ds.Tables["Enrollments"].PrimaryKey = new DataColumn[2]
                    { ds.Tables["Enrollments"].Columns["StId"], ds.Tables["Enrollments"].Columns["CId"] };

            // =========================================================================  
            

            ForeignKeyConstraint myFK01 = new ForeignKeyConstraint("MyFK01",
                new DataColumn[]{
                    ds.Tables["Courses"].Columns["CId"]
                },
                new DataColumn[] {
                    ds.Tables["Enrollments"].Columns["CId"]
                }
            );
            myFK01.DeleteRule = Rule.Cascade;
            myFK01.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(myFK01);

            ForeignKeyConstraint myFK02 = new ForeignKeyConstraint("MyFK02",
              new DataColumn[]{
                    ds.Tables["Students"].Columns["StId"]
              },
              new DataColumn[] {
                    ds.Tables["Enrollments"].Columns["StId"]
              }
          );
            myFK02.DeleteRule = Rule.None;
            myFK02.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(myFK02);

            // =========================================================================  
        }

        internal static SqlDataAdapter getAdapterProg()
        {
            return adapterProg;
        }
        internal static SqlDataAdapter getAdapterCo()
        {
            return adapterCo;
        }
        internal static SqlDataAdapter getAdapterSt()
        {
            return adapterSt;
        }
        internal static SqlDataAdapter getAdapterEn()
        {
            return adapterEn;
        }
        internal static DataSet getDataSet()
        {
            return ds;
        }
    }

    internal class Programs
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterProg();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetPrograms()
        {
            return ds.Tables["Programs"];
        }

        internal static int UpdatePrograms()
        {
            if (!ds.Tables["Programs"].HasErrors)
            {
                return adapter.Update(ds.Tables["Programs"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Courses
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterCo();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetCourses()
        {
            return ds.Tables["Courses"];
        }

        internal static int UpdateCourses()
        {
            if (!ds.Tables["Courses"].HasErrors)
            {
                return adapter.Update(ds.Tables["Courses"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Students
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterSt();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetStudents()
        {
            return ds.Tables["Students"];
        }

        internal static int UpdateStudents()
        {
            if (!ds.Tables["Students"].HasErrors)
            {
                return adapter.Update(ds.Tables["Students"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Enrollments
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterEn();
        private static DataSet ds = DataTables.getDataSet();

        private static DataTable displayEnroll = null;

        internal static DataTable GetDisplayEnrollments()
        {
           
            ds.Tables["Enrollments"].AcceptChanges();

            var query = (
                   from enroll in ds.Tables["Enrollments"].AsEnumerable()                   
                   from co in ds.Tables["Courses"].AsEnumerable()
                   from st in ds.Tables["Students"].AsEnumerable()                   
                   where enroll.Field<string>("CId") == co.Field<string>("CId")
                   where enroll.Field<string>("StId") == st.Field<string>("StId")
                   select new
                   {
                       CId = co.Field<string>("CId"),
                       CName = co.Field<string>("CName"),
                       StId = st.Field<string>("StId"),
                       StName = st.Field<string>("StName"),
                       FinalGrade = enroll.Field<Nullable<int>>("FinalGrade")
                   });
            DataTable result = new DataTable();
            result.Columns.Add("CId");
            result.Columns.Add("CName");
            result.Columns.Add("StId");
            result.Columns.Add("StName");
            result.Columns.Add("FinalGrade");
            foreach (var x in query)
            {
                object[] allFields = { x.CId, x.CName, x.StId, x.StName, x.FinalGrade };
                result.Rows.Add(allFields);
            }
            displayEnroll = result;
            return displayEnroll;
        }

        internal static int InsertData(string[] a)
        {
            var test = (
                   from assign in ds.Tables["Enrollments"].AsEnumerable()
                   where assign.Field<string>("CId") == a[0]
                   where assign.Field<string>("StId") == a[1]
                   select assign);
            if (test.Count() > 0)
            {
                Project.Form1.DALMessage("This assignment already exists");
                return -1;
            }
            try
            {
                DataRow line = ds.Tables["Enrollments"].NewRow();
                line.SetField("CId", a[0]);
                line.SetField("StId", a[1]);
                ds.Tables["Enrollments"].Rows.Add(line);

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnroll != null)
                {
                    var query = (
                           from co in ds.Tables["Courses"].AsEnumerable()
                           from st in ds.Tables["Students"].AsEnumerable()
                           where co.Field<string>("CId") == a[0]
                           where st.Field<string>("StId") == a[1]
                           select new
                           {
                               CId = co.Field<string>("CId"),
                               CName = co.Field<string>("CName"),
                               StId = st.Field<string>("StId"),
                               StName = st.Field<string>("StName"),
                               FinalGrade = line.Field<Nullable<int>>("FinalGrade")  
                           });
                    // Note that Eval =line.Field<Nullable<int>>("Eval") will 
                    // always place null in Eval. It is not needed. 
                    // It is enough to ommit Eval in the select and  
                    // ommit r.Eval in displayAssign.Rows.Add(...)
                    var r = query.Single();
                    displayEnroll.Rows.Add(new object[] { r.CId, r.CName, r.StId, r.StName, r.FinalGrade });
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Insertion / Update rejected");
                return -1;
            }
        }

        internal static int UpdateData(string[] a)
        {
            return 0;  //not used
        }

        internal static int DeleteData(List<string[]> lId)
        {
            try
            {
                var lines = ds.Tables["Enrollments"].AsEnumerable()
                                .Where(s =>
                                   lId.Any(x => (x[0] == s.Field<string>("CId") && x[1] == s.Field<string>("StId"))));

                foreach (var line in lines)
                {
                    line.Delete();
                }

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnroll != null)
                {
                    foreach (var p in lId)
                    {
                        var r = displayEnroll.AsEnumerable()
                                .Where(s => (s.Field<string>("CId") == p[0] && s.Field<string>("StId") == p[1]))
                                .Single();
                        displayEnroll.Rows.Remove(r);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }

        internal static int UpdateFinalGrade(string[] a, Nullable<int> finalgrade)
        {
            try
            {
                var line = ds.Tables["Enrollments"].AsEnumerable()
                                    .Where(s =>
                                      (s.Field<string>("CId") == a[0] && s.Field<string>("StId") == a[1]))
                                    .Single();

                line.SetField("FinalGrade", finalgrade);

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnroll != null)
                {
                    var r = displayEnroll.AsEnumerable()
                                    .Where(s =>
                                      (s.Field<string>("CId") == a[0] && s.Field<string>("StId") == a[1]))
                                    .Single();
                    r.SetField("FinalGrade", finalgrade);
                }
                return 0;  
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }
    }
}
