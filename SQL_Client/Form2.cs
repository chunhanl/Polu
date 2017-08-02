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
        private string path_stl_main = "";
        private string path_stl_mainstone = "";
        private string path_3dm = "";
        private bool isImgNeedUpdate = false;
        private bool isStlMainNeedUpdate =false;
        private bool isStlMainStoneNeedUpdate = false;
        private bool isStlSubStonesNeedUpdate = false;
        private bool is3dmNeedUpdate =false;
        private bool[] isStlModified;
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

           /* Form1 loginForm = new Form1();
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
            }*/


            InitializeComponent();

            //show account and password
            this.textBox1.Text = account;

            //initial sql
            sql = new SQL_Util();
            sql.IP = "127.0.0.1";//"118.170.189.76";
            sql.userAccount = "root";//account;
            sql.userPwd = "11111111";//password;
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

        private bool readSelectedIntoModel()
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
                return false;
            }

            float f = current_model.weight;
            double d = current_model.work_cost;
            if (!float.TryParse(textB_weight.ToString(), out f) && string.Equals(textB_weight, string.Empty))
            {
                MessageBox.Show("重量只能填數字喔");
                return false;
            }
            else
            {
                current_model.weight = f;
            }
            if (!double.TryParse(textB_cost.ToString(), out d) && string.Equals(textB_cost, string.Empty))
            {
                MessageBox.Show("重量只能填數字喔");
                return false;
            }
            else
            {
                current_model.work_cost = d;
            }
            current_model.comment = textB_comment.Text;

            if( current_model.stone=="none" && current_model.substoneMaterials[0] != "none")
            {
                MessageBox.Show(current_model.stone + current_model.substoneMaterials[0]);
                isStlSubStonesNeedUpdate = false;
                for (int i = 0; i < 5; i++)
                {
                    current_model.substoneMaterials[i] = "none";
                    current_model.modelstl_substone[i] = null;
                }
                MessageBox.Show("請先用主石模型欄位，再用副石模型欄位");
                return false;
            }


            return true;
        }

        private bool checkFilesAndUpdate()
        {
            if (isImgNeedUpdate == true)
            {
                if (current_model.preview_image != null)
                {
                    sql.updateImg(current_model.preview_image, int.Parse(current_model.id));
                }
                else {
                    MessageBox.Show("無預覽圖");
                    return false;
                }
            }
            if (is3dmNeedUpdate)
            {
                current_model.model3dm = path_3dm;
                if (current_model.model3dm != null)
                {
                    sql.update3dm(current_model.model3dm, int.Parse(current_model.id));
                }
                else
                {
                    MessageBox.Show("無3dm圖");
                    return false;
                }
            }

            if (isStlMainNeedUpdate)
            {
                current_model.modelstl_main = FileToByteArray(path_stl_main);
                if (current_model.modelstl_main != null)
                {
                    sql.updateStl(current_model.modelstl_main, int.Parse(current_model.id), SQL_Structure.Stl.main);
                }
                else
                {
                    MessageBox.Show("無stl圖");
                    return false;
                }
            }
            if (isStlMainStoneNeedUpdate)
            {
                current_model.modelstl_mainstone = FileToByteArray(path_stl_mainstone);
                if (current_model.modelstl_mainstone != null)
                {
                    sql.updateStl(current_model.modelstl_mainstone, int.Parse(current_model.id), SQL_Structure.Stl.mainstone);
                }
                else
                {
                    MessageBox.Show("無stl主石圖");
                    return false;
                }
            }

            if (isStlSubStonesNeedUpdate)
            {
                for(int i = 0; i < 5; i++)
                {
                    if(isStlModified[i])
                    {
                        sql.updateStl(current_model.modelstl_substone[i], current_model.substoneMaterials[i], int.Parse(current_model.id), i);
                    }
                }
            }
            
            return true;
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
                if (current_model.isExistInDB)
                {                
                    if( readSelectedIntoModel() == false)
                    {
                        //資料未輸入完全
                        return; 
                    }
                    this.status.BackColor = Color.Red;
                    this.status.Text = "入庫中";
                    this.Refresh();

                    sql.Update(current_model);
                    checkFilesAndUpdate();
                    
                    this.status.BackColor = Color.YellowGreen;
                    this.status.Text = "已入庫";
                    this.Refresh();
                }
                else
                {
                    if (readSelectedIntoModel() == false)
                    {
                        //資料未輸入完全
                        return;
                    }
                                        
                    checkFilesAndUpdate();
                    this.status.BackColor = Color.Red;
                    this.status.Text = "入庫中";
                    this.Refresh();

                    sql.Insert(current_model);
                    checkFilesAndUpdate();
                    
                    this.status.BackColor = Color.YellowGreen;
                    this.status.Text = "已入庫";
                    this.Refresh();
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
                    isStlMainNeedUpdate = false;
                    isStlMainStoneNeedUpdate = false;
                    isStlSubStonesNeedUpdate = false;
                    is3dmNeedUpdate = false;
                    isImgNeedUpdate = false;
                    isStlModified = new bool[5] { false,false,false,false,false};

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
                        isStlMainNeedUpdate = true;
                        isImgNeedUpdate = true;

                        //GET image from D:\ .....
                        Image img_tmp;
                        try
                        {
                            img_tmp = Image.FromFile(path_tmp + @"\" + path_tmp.Substring(dir_path.Length + 1) + ".jpg");
                            path_img = path_tmp + @"\" + path_tmp.Substring(dir_path.Length + 1) + ".jpg";
                        }
                        catch (Exception)
                        {
                            try
                            {
                                IEnumerable<string> files = Directory.GetFiles(path_tmp, "*.jpg", SearchOption.TopDirectoryOnly);
                                img_tmp = Image.FromFile(files.ElementAt<string>(0));
                                path_img = files.ElementAt<string>(0);
                                this.txtB_preimage.Text = path_img;
                            }
                            catch (Exception)
                            {
                                img_tmp = null;
                                setImgBox(null);
                                path_img = "無預覽圖";
                                this.txtB_preimage.Text = path_img;
                            }
                        }
                        setImgBox(img_tmp);               
                        current_model.preview_image = img_tmp;
                        this.txtB_preimage.Text = path_img;

                        //GET 3dm stl from D:\ .....
                        try
                        {
                            IEnumerable<string> files = Directory.GetFiles(path_tmp, "*.3dm", SearchOption.TopDirectoryOnly);
                            path_3dm = files.ElementAt<string>(0);
                            IEnumerable<string> files2 = Directory.GetFiles(path_tmp, "*.stl", SearchOption.TopDirectoryOnly);
                            path_stl_main = files2.ElementAt<string>(0);
                            this.txtB_3dm.Text = path_3dm;
                            this.txtB_stl.Text = path_stl_main;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("找不到3dm stl檔案，請手動選取^^");
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
            list_category.Items.Add("手鍊");
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
            if (current_model == null)
            {
                MessageBox.Show("請載入資料");
                return;
            }
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

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if (current_model == null)
            {
                MessageBox.Show("請載入資料");
                return;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //如果存在模型資料夾 開啟資料夾為預設位置
            if(Directory.Exists(dir_path + @"\" + current_model.modelID)){
                openFileDialog1.InitialDirectory = dir_path + @"\" + current_model.modelID;
                openFileDialog1.Filter = "JPG files| *.jpg";
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
        private void btn_3dm_Click(object sender, EventArgs e)
        {
            if (current_model == null)
            {
                MessageBox.Show("請載入資料");
                return;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (Directory.Exists(dir_path + @"\" + current_model.modelID))
            {
                openFileDialog1.InitialDirectory = dir_path + @"\" + current_model.modelID;
                openFileDialog1.Filter = "3dm files| *.3dm";
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
        private void btn_stlmain_Click(object sender, EventArgs e)
        {
            if (current_model == null)
            {
                MessageBox.Show("請載入資料");
                return;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (Directory.Exists(dir_path + @"\" + current_model.modelID))
            {
                openFileDialog1.InitialDirectory = dir_path + @"\" + current_model.modelID;
                openFileDialog1.Filter = "Stl files| *.stl";
            }
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                path_stl_main = openFileDialog1.FileName;
                this.txtB_stl.Text = path_stl_main;
                isStlMainNeedUpdate = true;
            }
            openFileDialog1.Dispose();
        }
        private void btn_stlmainstone_Click(object sender, EventArgs e)
        {
            if (current_model == null)
            {
                MessageBox.Show("請載入資料");
                return;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (Directory.Exists(dir_path + @"\" + current_model.modelID))
            {
                openFileDialog1.InitialDirectory = dir_path + @"\" + current_model.modelID;
                openFileDialog1.Filter = "Stl files| *.stl";
            }
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                path_stl_mainstone = openFileDialog1.FileName;
                this.textBox2.Text = path_stl_mainstone;
                isStlMainStoneNeedUpdate = true;
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

        private void btn_morestones_Click(object sender, EventArgs e)
        {
            if (current_model == null)
            {
                MessageBox.Show("請載入資料");
                return;
            }
            Form4 substoneForm = new Form4( current_model );
            substoneForm.default_folder_path = dir_path + @"\" + current_model.modelID;

            if (substoneForm.ShowDialog() == DialogResult.OK)
            {
                isStlSubStonesNeedUpdate = substoneForm.isModified;
                if (isStlSubStonesNeedUpdate)
                {
                    Array.Copy(substoneForm.isEachModified, this.isStlModified, 5);
                    Array.Copy(current_model.substoneMaterials , substoneForm.material , 5);
                    Array.Copy(current_model.modelstl_substone, substoneForm.stl, 5);

                }
                substoneForm.Close();
                return;
            }
            else
            {
                substoneForm.Close();
            }
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
