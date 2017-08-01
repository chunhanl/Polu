using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;

namespace SQL_Client
{
    public partial class Form2 : Form
    {
        //temp
        private string dir_path = @"D:\Polu_DB\Models";
        private string path_img = "";
        private string path_stl = "";
        private string path_3dm = "";
        private bool isImgNeedUpdate = false;
        private bool isStlNeedUpdate =false;
        private bool is3dmNeedUpdate =false;
        IEnumerable<string> model_dirs;

        private int current_pos = 0;
        //data member
        private string account;
        private string password;
        private SQL_Util sql;
        private SQL_Structure current_model;

        
        private Form3 stoneform;
        private bool stoneInfoUpdate = false;

        //constructor
        public Form2()
        {
            //init variables
            account = string.Empty;
            password = string.Empty;
            //ask user to login
            Form1 loginForm = new Form1();
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                Environment.Exit(Environment.ExitCode);
                return;
            }
            else
            {
                account = loginForm.accountStr();
                password = loginForm.passwordStr();
                loginForm.Close();
            }


            InitializeComponent();

            //show account and password
            this.textBox1.Text = account;

            //initial sql
            sql = new SQL_Util();
            sql.IP = "192.168.1.247";
            sql.userAccount = account;//account;
            sql.userPwd = password;//password;
            sql.database = "sys";

            sql.table = "main_table";
            if (sql.OpenConnection() == false)
            {
                MessageBox.Show("與資料庫連線失敗，請重新開啟");
                this.Shown += new EventHandler(CloseOnStart);
            }
            else
            {
                try
                {
                    var dirs = Directory.EnumerateDirectories(dir_path);
                    model_dirs = dirs;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                this.textBox1.Text = sql.userAccount;
                this.showListBox();
            }
            
        }

        private void CloseOnStart(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            if (current_model==null)
            {
                MessageBox.Show("請載入資料");
                return;
            }

            if (sql.checkConnection())
            {
                //Insert(string model_id,string category,float weight,double workcost,string producer,string gender, string stone)
         
                if (current_model.isExistInDB)
                {
                    if (list_category.SelectedIndex != -1 && list_gender.SelectedIndex != -1 && list_manufact.SelectedIndex != -1)
                    {
                        current_model.category = Enum.GetName(typeof(SQL_Structure.Category), list_category.SelectedIndex);
                        current_model.manufacture = Enum.GetName(typeof(SQL_Structure.Manufacture), list_manufact.SelectedIndex);
                        current_model.gender = Enum.GetName(typeof(SQL_Structure.Gender), list_gender.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show("前三格選一下拜託~~");
                        return;
                    }

                    float f = current_model.weight;
                    double d = current_model.work_cost;
                    if (!float.TryParse(textB_weight.ToString(), out f) && string.Equals(textB_weight, string.Empty))
                    {
                        MessageBox.Show("重量只能填數字喔");
                        return;
                    }
                    else
                    {
                        current_model.weight = f;
                    }
                    if (!double.TryParse(textB_cost.ToString(), out d) && string.Equals(textB_cost, string.Empty))
                    {
                        MessageBox.Show("重量只能填數字喔");
                        return;
                    }
                    else
                    {
                        current_model.work_cost = d;
                    }
                    current_model.comment = textB_comment.Text;
                    this.status.BackColor = Color.Red;
                    this.status.Text = "入庫中";
                    this.Refresh();
                    sql.Update(current_model);


                    if (isImgNeedUpdate == true)
                    {
                        sql.updateImg(current_model.preview_image, int.Parse(current_model.id));
                    }
                    if (is3dmNeedUpdate)
                    {
                        current_model.model3dm = path_3dm;
                        if (current_model.model3dm != null)
                        {
                            sql.update3dm(current_model.model3dm, int.Parse(current_model.id));
                        }
                    }
                     
                    if (isStlNeedUpdate)
                    {
                        current_model.modelstl = FileToByteArray(path_stl);
                        if (current_model.modelstl != null)
                        {
                            sql.updateStl(current_model.modelstl, int.Parse(current_model.id));
                        }
                    }
                    this.status.BackColor = Color.YellowGreen;
                    this.status.Text = "已入庫";
                }
                else
                {
                    if (list_category.SelectedIndex != -1 && list_gender.SelectedIndex != -1 && list_manufact.SelectedIndex != -1)
                    {
                        current_model.category = Enum.GetName(typeof(SQL_Structure.Category), list_category.SelectedIndex);
                        current_model.manufacture = Enum.GetName(typeof(SQL_Structure.Manufacture), list_manufact.SelectedIndex);
                        current_model.gender = Enum.GetName(typeof(SQL_Structure.Gender), list_gender.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show("前三格選一下拜託~~");
                        return;
                    }

                    float f = 0;
                    double d = 0;
                    if (!float.TryParse(textB_weight.ToString(), out f) && string.Equals(textB_weight, string.Empty))
                    {
                        MessageBox.Show("重量只能填數字喔");
                        return;
                    }
                    else
                    {
                        current_model.weight = f;
                    }
                    if (!double.TryParse(textB_cost.ToString(), out d) && string.Equals(textB_cost, string.Empty))
                    {
                        MessageBox.Show("重量只能填數字喔");
                        return;
                    }
                    else
                    {
                        current_model.work_cost = d;
                    }
                    current_model.comment = textB_comment.Text;

                    current_model.model3dm = path_3dm;
                    current_model.modelstl = FileToByteArray(path_stl);
                    if (current_model.model3dm == null || current_model.modelstl == null)
                    {
                        MessageBox.Show("模型要補齊喔");
                        return;
                    }


                    this.status.BackColor = Color.Red;
                    this.status.Text = "入庫中";
                    this.Refresh();
                    sql.Insert(current_model);
                     
                    if (isImgNeedUpdate == true)
                    {
                        sql.updateImg(current_model.preview_image, int.Parse(current_model.id));
                    }
                     
                    sql.update3dm(current_model.model3dm, int.Parse(current_model.id));
                     
                    sql.updateStl(current_model.modelstl, int.Parse(current_model.id));
                     
                    
                    this.status.BackColor = Color.YellowGreen;
                    this.status.Text = "已入庫";
                }
            }
            else
            {
                MessageBox.Show("與資料庫連線失敗，請重新開啟");
            }
        }


         
        private void GetNext_Click(object sender, EventArgs e)
        {            
            if (sql.checkConnection() )
            {
                try
                {
                    string path_tmp = model_dirs.ElementAt<string>(current_pos);
                    string ImportModelID = path_tmp.Substring(dir_path.Length + 1);
                    isStlNeedUpdate = false;
                    is3dmNeedUpdate = false;
                    isImgNeedUpdate = false;

                    if (!sql.isModelidExist(ImportModelID))
                    {
                        current_model = new SQL_Structure();
                        current_model.modelID = ImportModelID;

                        this.id.Text = "新模型";
                        this.modelID.Text = ImportModelID;
                        this.textB_cost.Text = "0";
                        this.textB_weight.Text = "0";
                        this.list_category.ClearSelected();
                        this.list_gender.ClearSelected();
                        this.list_manufact.ClearSelected();
                        stoneInfoRefresh();
                        this.textB_comment.Text = "";
                        this.status.BackColor = Color.Yellow;
                        this.status.Text = "未入庫";
                        this.txt_LMT.Text = "未入庫";
                        is3dmNeedUpdate = true;
                        isStlNeedUpdate = true;
                        //MessageBox.Show(current_model.modelID + " is new");

                        //GET image from D:\ .....
                        try
                        {
                            Image img_tmp = Image.FromFile(path_tmp + @"\" + path_tmp.Substring(dir_path.Length + 1) + ".jpg");
                            setImgBox(img_tmp);
                            current_model.preview_image = img_tmp;
                            path_img = path_tmp + @"\" + path_tmp.Substring(dir_path.Length + 1) + ".jpg";
                            this.txtB_preimage.Text = path_img;
                            isImgNeedUpdate = true;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                IEnumerable<string> files = Directory.GetFiles(path_tmp, "*.jpg", SearchOption.TopDirectoryOnly);
                                Image img_tmp = Image.FromFile(files.ElementAt<string>(0));
                                setImgBox(img_tmp);
                                current_model.preview_image = img_tmp;
                                path_img = files.ElementAt<string>(0);
                                this.txtB_preimage.Text = path_img;
                                isImgNeedUpdate = true;
                            }
                            catch (Exception)
                            {
                                setImgBox(null);
                                path_img = "無預覽圖";
                                this.txtB_preimage.Text = path_img;
                            }
                        }

                        //GET 3dm stl from D:\ .....
                        try
                        {
                            IEnumerable<string> files = Directory.GetFiles(path_tmp, "*.3dm", SearchOption.TopDirectoryOnly);
                            path_3dm = files.ElementAt<string>(0);
                            IEnumerable<string> files2 = Directory.GetFiles(path_tmp, "*.stl", SearchOption.TopDirectoryOnly);
                            path_stl = files2.ElementAt<string>(0);
                            this.txtB_3dm.Text = path_3dm;
                            this.txtB_stl.Text = path_stl;
                        }
                        catch (Exception)
                        {
                        }



                    }
                    else
                    {
                        current_model = sql.Read(ImportModelID );
                        current_model.isExistInDB = true;
                        this.id.Text = current_model.id;
                        this.modelID.Text = current_model.modelID;
                        this.textB_cost.Text = current_model.weight.ToString();
                        this.textB_weight.Text = current_model.weight.ToString();
                        int s1 = (int)Enum.Parse(typeof(SQL_Structure.Category), current_model.category);
                        this.list_category.SetSelected(s1,true);
                        int s2 = (int)Enum.Parse(typeof(SQL_Structure.Gender), current_model.gender);
                        this.list_gender.SetSelected(s2, true);
                        int s3 = (int)Enum.Parse(typeof(SQL_Structure.Manufacture), current_model.manufacture);
                        this.list_manufact.SetSelected(s3, true);
                        stoneInfoRefresh();
                        this.textB_comment.Text = current_model.comment;
                        this.status.BackColor = Color.YellowGreen;
                        this.status.Text = "已入庫";
                        this.txt_LMT.Text = current_model.LMT;

                        //MessageBox.Show(current_model.modelID + " is not new");
                        try
                        {
                            if (current_model.preview_image != null)
                            {
                                setImgBox(current_model.preview_image);
                                path_img = current_model.preview_image.ToString();
                                this.txtB_preimage.Text = "資料庫";
                            }
                            else
                            {
                                Image img_tmp = Image.FromFile(path_tmp + @"\" + path_tmp.Substring(dir_path.Length + 1) + ".jpg");
                                setImgBox(img_tmp);
                                current_model.preview_image = img_tmp;
                                path_img  = path_tmp + @"\" + path_tmp.Substring(dir_path.Length + 1) + ".jpg";
                                this.txtB_preimage.Text = path_img;
                                isImgNeedUpdate = true;
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                IEnumerable<string> files = Directory.GetFiles(path_tmp, "*.jpg", SearchOption.TopDirectoryOnly);
                                Image img_tmp = Image.FromFile(files.ElementAt<string>(0));
                                setImgBox(img_tmp);
                                current_model.preview_image = img_tmp;
                                path_img = files.ElementAt<string>(0);
                                this.txtB_preimage.Text = path_img;
                                isImgNeedUpdate = true;
                            }
                            catch (Exception)
                            {
                                setImgBox(null);
                                path_img = "無預覽圖";
                                this.txtB_preimage.Text = path_img;
                            }
                        }

                        //GET 3dm stl from D:\ .....
                        this.txtB_3dm.Text = "資料庫";
                        this.txtB_stl.Text = "資料庫";




                    }
                    current_pos++;
                }
                catch (ArgumentOutOfRangeException ex)
                {                               
                    Console.Write(ex);
                    MessageBox.Show("End of file");
                    return;
                }
            }
            else
            {
                MessageBox.Show("與資料庫連線失敗，請重新開啟");
            }
        }

        private void stoneInfoRefresh()
        {
            this.textBox3.Text = "";
            this.textBox3.Text += "石頭資訊:";
            this.textBox3.Text += Environment.NewLine;
            this.textBox3.Text += "種類：";
            this.textBox3.Text += current_model.stone;
            this.textBox3.Text += Environment.NewLine;
            this.textBox3.Text += "形狀：";
            this.textBox3.Text += current_model.stone_shape;
            this.textBox3.Text += Environment.NewLine;
            this.textBox3.Text += "大小：";
            this.textBox3.Text += current_model.stone_size;
            this.textBox3.Text += Environment.NewLine;
        }



        private void showListBox()
        {
            list_category.Items.Add("戒指");
            list_category.Items.Add("墜子");
            list_category.Items.Add("手環");
            list_category.Items.Add("手鐲");
            list_category.Items.Add("耳環");
            list_category.Items.Add("項鍊");
            list_category.Items.Add("套鍊");
            list_category.Items.Add("其他");

            list_gender.Items.Add("男生");
            list_gender.Items.Add("女生");
            list_gender.Items.Add("情侶");
            list_gender.Items.Add("皆可");

            list_manufact.Items.Add("台製");
            list_manufact.Items.Add("陸製");
            list_manufact.Items.Add("無");
        }


 

        private void LoadImg_Click(object sender, EventArgs e)
        {
            Image img = sql.LoadImage();
            imgBox.Image = img;
        }

        private void SendImg2Sql_Click(object sender, EventArgs e)
        {
            Image img = imgBox.Image;
            int val = Convert.ToInt32(this.id.Text);
            sql.updateImg(img, val);
        }

        private void setImgBox(Image img)
        {
            if(imgBox.Image != null)
            {
                //release memory usage
                imgBox.Image.Dispose();
            }
            imgBox.Image = img;
        }

        private void clearImgBox()
        {
            if (imgBox.Image != null)
            {
                //release memory usage
                imgBox.Image.Dispose();
                imgBox.Image = null;
            }
        }

        private void btn_stone_Click(object sender, EventArgs e)
        {
            if (current_model == null)
            {
                MessageBox.Show("請載入資料");
                return;
            }
            
            stoneform = new Form3();
            var v =stoneform.ShowDialog();
            stoneInfoUpdate = (v == DialogResult.OK);
            if (stoneInfoUpdate)
            {
                current_model.stone = Enum.GetName(typeof(SQL_Structure.Stone) ,stoneform.selected_1);
                current_model.stone_shape = stoneform.shape;
                current_model.stone_size = stoneform.size;
            }
            stoneform.Dispose();
            stoneInfoRefresh();
        }

        private void btn_pre_Click(object sender, EventArgs e)
        {
            if (current_pos >= 2)
            {
                current_pos -= 2;
                GetNext_Click(sender, e);
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //如果存在模型資料夾 開啟資料夾為預設位置
            if(Directory.Exists(dir_path + @"\" + current_model.modelID)){
                openFileDialog1.InitialDirectory = dir_path + @"\" + current_model.modelID;
            }
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                path_img = openFileDialog1.FileName;
                current_model.preview_image = Image.FromFile(path_img);
                setImgBox(current_model.preview_image);
                isImgNeedUpdate = true;
                this.txtB_preimage.Text = path_img;
            }
            openFileDialog1.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (Directory.Exists(dir_path + @"\" + current_model.modelID))
            {
                openFileDialog1.InitialDirectory = dir_path + @"\" + current_model.modelID;
            }
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                path_3dm = openFileDialog1.FileName;
                this.txtB_3dm.Text = path_3dm;
                is3dmNeedUpdate = true;
            }
            openFileDialog1.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (Directory.Exists(dir_path + @"\" + current_model.modelID))
            {
                openFileDialog1.InitialDirectory = dir_path + @"\" + current_model.modelID;
            }
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                path_stl = openFileDialog1.FileName;
                this.txtB_stl.Text = path_stl;
                isStlNeedUpdate = true;
            }
            openFileDialog1.Dispose();
        }



        private byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                                   FileMode.Open,
                                   FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        //old testing
        /*
        private void FetchData_Click(object sender, EventArgs e)
        {
            //get data from sql database(db)
            string dbIP = "127.0.0.1";
            string dbUser = "swallow";
            string dbPass = "collin24";
            string dbName = "testdatabase";
            string dbTable = "jewelry";
            string connStr = "server=" + dbIP + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Fetch all data!");
                command.CommandText = "select * from " + dbTable + ";";
                MySqlDataReader reader = command.ExecuteReader();
                                  
                string data = string.Empty;
                while (reader.Read())
                {
                    for (int col = 0; col < 3; col++)
                    {
                        data += (reader.GetString(col) + "  ");
                    }
                    data += System.Environment.NewLine;
                }

                this.ShowBox.Text = data;
                reader.Close();
                conn.Close();
            }
            else  //failed to open
            {
                MessageBox.Show("failed to reach sql server!");
                conn.Close();
            }


        }

        private void Add10Data_Click(object sender, EventArgs e)
        {
            //get data from sql database(db)
            string dbIP = "127.0.0.1";
            string dbUser = "swallow";
            string dbPass = "collin24";
            string dbName = "testdatabase";
            string dbTable = "jewelry";
            string connStr = "server=" + dbIP + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            if(conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Add 10 data into data base");
                for(int i=0; i<10; i++)
                {
                    int id = i + 3;
                    string jewel_Name = "jewel_" + i.ToString();
                    float price = i * 10000;
                    command.CommandText = "insert into " + dbTable + "(id, jewel_name, price) " + "value(" + id.ToString() + ", '" + jewel_Name.ToString() + "', " + price.ToString() + ");";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            else  //failed to open
            {
                MessageBox.Show("failed to reach sql server!");
                conn.Close();
            }
        }
        */
    }
}
