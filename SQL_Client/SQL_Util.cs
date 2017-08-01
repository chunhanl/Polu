using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;


namespace SQL_Client
{
    class SQL_Util
    {
        //data member
        public string IP { get; set;}
        public string userAccount { get; set; }
        public string userPwd { get; set; }
        public string database { get; set; }
        public string table { get; set; }

        private MySqlConnection conn;
        private MySqlCommand command;
        private MySqlDataAdapter adapter;

        private bool connectionSuccess;

        //constructor
        public SQL_Util()
        {
            connectionSuccess = false;
            IP = string.Empty;
            userAccount = string.Empty;
            userPwd = string.Empty;
            database = string.Empty;
            table = string.Empty;
        }

        //data checking
        private bool hasEmptyValue()
        {
            if(IP == string.Empty)
            {
                return true;
            }

            if(userAccount == string.Empty)
            {
                return true;
            }

            if(userPwd == string.Empty)
            {
                return true;
            }

            if(database == string.Empty)
            {
                return true;
            }

            if(table == string.Empty)
            {
                return true;
            }

            return false;   
        }

        //SQL commands
        public bool OpenConnection()
        {
            if (hasEmptyValue() == false)
            {
                string connStr = "server=" + IP + ";uid=" + userAccount + ";pwd=" + userPwd + ";database=" + database;
                conn = new MySqlConnection(connStr);
                command = conn.CreateCommand();
                connectionSuccess = checkConnection();
            }
            return connectionSuccess;
        }

        public bool checkConnection()
        {
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Insert(SQL_Structure ss)
        {
            
            //INSERT INTO `sys`.`main_table` (`model_id`, `category`, `weight`, `work_cost`, `producer`, `gender`, `stone`, `last_modified_time`, `last_modified_user`) VALUES('123456', 'earring', '50', '545', 'china', 'both', 'diamond', '2017/07/17 12:31:52', 'root');
            conn.Open();
            command.CommandText = "insert into " + database + "." + table 
                                    + " (`model_id`, `category`, `weight`, `work_cost`, `producer`, `gender`, `stone`, `last_modified_time`, `last_modified_user`, `comment`) values('" + ss.modelID+"','"+ss.category+"','"+ss.weight+"','"+ss.work_cost+"','"+ss.manufacture+"','"+ss.gender+"','"+ss.stone+"','" + DateTime.Now.ToString("yyyy.MMM.dd HH:mm:ss") + "','" +userAccount+"','"+ss.comment+"')";
            command.ExecuteNonQuery();


            command.CommandText = "select * from " + database + "." + table + " order by id desc limit 1";
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string id = reader.GetString(0);
            reader.Dispose();
            ss.id = id;

            if (ss.stone != "none")
            {                
                //INSERT INTO `sys`.`stone_table` (`local_id`, `model_id`, `shape`, `size`) VALUES(65536, 32, 'oval', 'less30');
                command.CommandText = "insert into " + database + ".stone_table"
                                        + " (`parent_id`, `shape`, `size`) values('" + ss.id + "','" + ss.stone_shape + "','" + ss.stone_size + "')";
                command.ExecuteNonQuery();
            }

            conn.Close();
            
            ss.isExistInDB = true;
            return;
        }



        public void Update(SQL_Structure ss)
        {
            //UPDATE `sys`.`main_table` SET `category`='earring', `weight`='4', `work_cost`='4', `producer`='taiwan', `gender`='male', `stone`='jade', `last_modified_time`='asd' WHERE `id`='3';
            bool b = isIdExistInStone(ss.id);

            conn.Open();
            command.CommandText = "update " + database + "." + table
                                    + " set `category`='"+ss.category+"', `weight`='"+ss.weight+"', `work_cost`='"+ss.work_cost+
                                    "', `producer`='"+ss.manufacture+"', `gender`='"+ss.gender+"', `stone`='"+ss.stone+ "', `last_modified_time`='" + DateTime.Now.ToString("yyyy.MMM.dd HH:mm:ss") + "', `comment`='" + ss.comment +"' where `model_id`='"+ss.modelID+"'" ;
            Console.Write(command.CommandText);
            command.ExecuteNonQuery();


            
            if (b == true && ss.stone == "none")
            {
                command.CommandText = " DELETE FROM " + database + ".stone_table" + " WHERE `parent_id` ='" + ss.id + "'";
                command.ExecuteNonQuery();
            }
            else if(b!=true &&ss.stone!="none")
            {
                command.CommandText = "insert into " + database + ".stone_table" + " (`parent_id`, `shape`, `size`) values('" + ss.id + "','" + ss.stone_shape + "','" + ss.stone_size + "')";
                command.ExecuteNonQuery();          
            }
            else if(b==true && ss.stone !="none")
            {
                command.CommandText = "UPDATE " + database + ".stone_table" + " SET `shape`='" + ss.stone_shape + "', `size`='" + ss.stone_size + "' WHERE `parent_id` = '" + ss.id + "'";
                command.ExecuteNonQuery();
            }
            else {
                ;
            }
            


            //MessageBox.Show(command.CommandText);
            conn.Close();
            return;
        }

        public bool updateImg(Image img, int id)
        {
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            byte[] arr = ImageToBlob(img);
            cmd.CommandText = "update " + table + " set design_img=" + "@img" + " where id=" + id.ToString();
            cmd.Parameters.Add("@img", MySqlDbType.MediumBlob);
            cmd.Parameters["@img"].Value = arr;

            if(cmd.ExecuteNonQuery() == 1)
            {
                //MessageBox.Show("Data replaced");
            }
            else
            {
                //MessageBox.Show("Data not replaced");
            }
            
            cmd.CommandText = "update " + table + " set design_img_icon=" + "@img2" + " where id=" + id.ToString();
            cmd.Parameters.Add("@img2", MySqlDbType.MediumBlob);
            cmd.Parameters["@img2"].Value = arr;
            if (cmd.ExecuteNonQuery() == 1)
            {
                //MessageBox.Show("Data replaced");
            }
            else
            {
                //MessageBox.Show("Data not replaced");
            }
            
            conn.Close();
            return true;
        }


        public bool updateStl(byte[] arr, int id, SQL_Structure.Stl option)
        {
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            try
            {
                cmd.CommandText = "INSERT INTO `sys`.`model_table` (`parent_id`) VALUES( " + id.ToString() + ")";
                cmd.ExecuteNonQuery();
            }catch(Exception)
            {
                MessageBox.Show("資料庫出問題，請洽苦命的工程師 ErrorA01");
            }

            switch(option){
                case SQL_Structure.Stl.main :
                    cmd.CommandText = "update " + "sys.model_table" + " set design_model_stl=" 
                                            + "@stl" + " where parent_id=" + id.ToString();
                    break;
                case SQL_Structure.Stl.mainstone:
                    cmd.CommandText = "update " + "sys.model_table" + " set design_model_stl_mainstone="
                                        + "@stl" + " where parent_id=" + id.ToString();
                    break;
                case SQL_Structure.Stl.substone:
                    cmd.CommandText = "update " + "sys.model_table" + " set design_model_stl_substone="
                                        + "@stl" + " where parent_id=" + id.ToString();
                    break;
                default:
                    conn.Close();
                    return false;                    
            }
            cmd.Parameters.Add("@stl", MySqlDbType.MediumBlob);
            cmd.Parameters["@stl"].Value = arr;
            cmd.ExecuteNonQuery();
            
            conn.Close();
            return true;
        }

        public bool update3dm(string arr, int id)
        {
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            try { 
                cmd.CommandText = "INSERT INTO `sys`.`model_table` (`parent_id`) VALUES( " + id.ToString() + ")";
                cmd.ExecuteNonQuery();
            }catch(Exception)
            {
                ;
            }
            
            cmd.CommandText = "update " + "sys.model_table" + " set design_model_3dm=" + "@mdl" + " where parent_id=" + id.ToString();
            cmd.Parameters.Add("@mdl", MySqlDbType.MediumText);
            cmd.Parameters["@mdl"].Value = arr;
            cmd.ExecuteNonQuery();

            conn.Close();
            return true;
        }

        //SELECT EXISTS(SELECT* FROM sys.main_table WHERE model_id= "050050");

        public bool isModelidExist(string m_id)
        {
            conn.Open();
            command.CommandText = "select exists(select * from " + database+ "." +table + " where model_id='" + m_id +"' )";
            Console.Write(command.CommandText);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            bool b = reader.GetString(0)== "1"?true:false;
            reader.Dispose();
            conn.Close();
            return b;
        }


        public bool isIdExistInStone(string id)
        {
            conn.Open();
            command.CommandText = "select exists(select * from " + database + ".stone_table" + " where parent_id='" + id + "' )";
            Console.Write(command.CommandText);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            bool b = reader.GetString(0) == "1" ? true : false;
            reader.Dispose();
            conn.Close();
            return b;
        }



        public SQL_Structure Read(string model_id)
        {
            SQL_Structure sql_struct = new SQL_Structure();
            conn.Open();
            /*
            if(conn.State != ConnectionState.Open)
            {
                conn.Close();
                return sql_struct;
            }
            */
            //add query
            command.CommandText = "select * from " + database + "." + table + " where model_id='" + model_id + "'";
            adapter = new MySqlDataAdapter(command);
            DataTable t = new DataTable();
            adapter.Fill(t);
            //fill structure
            sql_struct.id = t.Rows[0][0].ToString();
            sql_struct.modelID = t.Rows[0][1].ToString();
            sql_struct.category = t.Rows[0][2].ToString();
            sql_struct.weight = (float)t.Rows[0][3];
            sql_struct.work_cost = (double)t.Rows[0][4];
            sql_struct.manufacture = t.Rows[0][5].ToString();
            sql_struct.gender = t.Rows[0][6].ToString();
            sql_struct.stone = t.Rows[0][7].ToString();
            sql_struct.LMT = t.Rows[0][9].ToString();
            sql_struct.comment = t.Rows[0][12].ToString();
            if (t.Rows[0][13] != System.DBNull.Value)
            {
                sql_struct.preview_image = BlobToImage((byte[])t.Rows[0][13]);
            }
            //release memory use by adapter
            adapter.Dispose();
            t.Dispose();

            if (sql_struct.stone != "none")
            {
                command.CommandText = "select * from " + database + "." + "stone_table" + " where parent_id='" + sql_struct.id + "'";
                adapter = new MySqlDataAdapter(command);
                t = new DataTable();
                adapter.Fill(t);
                sql_struct.stone_shape  = t.Rows[0][2].ToString();
                sql_struct.stone_size  = t.Rows[0][3].ToString();
                adapter.Dispose();
                t.Dispose();
            }


            conn.Close();
            return sql_struct;
        }



        //Image functions
        public Image LoadImage()
        {
            string path = getImageNameFromDialog();
            return ReadImage(path);
        }

        public Image ReadImage(string name)
        {
            return Image.FromFile(name);
        }

        public byte[] ImageToBlob(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            return ms.ToArray();
        }

        public Image BlobToImage(byte[] bArr)
        {
            MemoryStream ms = new MemoryStream(bArr);
            return Image.FromStream(ms);
        }

        //interaction functions
        public string getImageNameFromDialog()
        {
            string fileName = string.Empty;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "image files (*.bmp, *.jpg)|*.bmp;*.jpg;";
            DialogResult result = fileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                if (fileDialog.CheckFileExists == true)
                {
                    fileName = fileDialog.FileName;
                }
            }
            return fileName;
        }
    }

    class SQL_Structure
    {
        //data member
        public string id { get; set; }
        public string modelID { get; set; }
        public enum Gender { male, female, couple, both, notgiven };
        public enum Category { ring, pendant, wristband, bracelet, earring, necklace, set, other };
        public enum Manufacture { taiwan, china, notgiven };
        public enum Stone { diamond, jade, redblue, pearl, other, none };
        public enum StoneShape {none, drop, heart, circle, oval, rect, eye, god, other};
        public enum Stl {main, mainstone, substone };

        public string gender { get; set; }
        public string category { get; set; }
        public string manufacture { get; set; }
        public string stone { get; set; }
        public string stone_shape { get; set; }
        public string stone_size { get; set; }
        public float weight { get; set; }
        public double work_cost { get; set; }
        public string LMT { get; set; } //Last Modified Time
        public bool isExistInDB;
        public string comment;

        public Image preview_image;
        public string model3dm;
        public byte[] modelstl_main;
        public byte[] modelstl_mainstone;
        public byte[] modelstl_substone;


        public SQL_Structure()
        {
            this.stone = "none";
            this.stone_shape = "none";
            this.stone_size = "notgiven";
            this.weight = 0;
            this.work_cost = 0;
            this.isExistInDB = false;
        }

    }
}
