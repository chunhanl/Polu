using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Client
{
    public partial class Form4 : Form
    {
        public int numofSubExist { get; set; }
        public enum ModelStatus { empty, exist }; // empty = originally empty; exist = originally exist ; modified = modified
        public ModelStatus[] status { get; set; }
        public byte[][] stl { get; set; }
        public string[] path { get; set; }
        public string[] material { get; set; }
        public string default_folder_path;

        public bool isModified;        // Tell the Caller(Form2) whether there's any changes
        public bool[] isEachModified;

        public Form4(SQL_Structure model)
        {
            InitializeComponent();
            numofSubExist = 0;
            status = new ModelStatus[5];
            stl = new byte[5][];
            path = new string[5];
            material = new string[5];
            isEachModified = new bool[5];
            for (int i = 0; i < 5; i++) isEachModified[i] = false;
            LoadModelData(model);
            isModified = false;
        }

        private void LoadModelData(SQL_Structure model)
        {
            //New unimported to DB Model
            if (model.isExistInDB == false)
            {
                numofSubExist = 0;
                listBox1.SetSelected(0, true);
                listBox2.SetSelected(0, true);
                listBox3.SetSelected(0, true);
                listBox4.SetSelected(0, true);
                listBox5.SetSelected(0, true);
                for (int i = 0; i < 5; i++)
                {
                    status[i] = ModelStatus.empty;
                }
            }
            else
            {
                if (model.substoneMaterials[0]== Enum.GetName(typeof(SQL_Structure.StlMaterial),SQL_Structure.StlMaterial.none))
                {   //no model
                    listBox1.SetSelected(0, true);
                    status[0] = ModelStatus.empty;
                }
                else
                {   //has model
                    textBox1.Text = "資料庫";
                    int s1 = (int)Enum.Parse(typeof(SQL_Structure.StlMaterial), model.substoneMaterials[0]);
                    listBox1.SetSelected(s1, true);
                    status[0] = ModelStatus.exist;
                }

                if (model.substoneMaterials[1] == Enum.GetName(typeof(SQL_Structure.StlMaterial), SQL_Structure.StlMaterial.none))
                {
                    listBox2.SetSelected(0, true);
                    status[1] = ModelStatus.empty;
                }
                else
                {
                    textBox2.Text = "資料庫";
                    int s1 = (int)Enum.Parse(typeof(SQL_Structure.StlMaterial), model.substoneMaterials[1]);
                    listBox2.SetSelected(s1, true);
                    status[1] = ModelStatus.exist;
                }

                if (model.substoneMaterials[2] == Enum.GetName(typeof(SQL_Structure.StlMaterial), SQL_Structure.StlMaterial.none))
                {
                    listBox3.SetSelected(0, true);
                    status[2] = ModelStatus.empty;
                }
                else
                {
                    textBox3.Text = "資料庫";
                    int s1 = (int)Enum.Parse(typeof(SQL_Structure.StlMaterial), model.substoneMaterials[2]);
                    listBox3.SetSelected(s1, true);
                    status[2] = ModelStatus.exist;
                }

                if (model.substoneMaterials[3] == Enum.GetName(typeof(SQL_Structure.StlMaterial), SQL_Structure.StlMaterial.none))
                {
                    listBox4.SetSelected(0, true);
                    status[3] = ModelStatus.empty;
                }
                else
                {
                    textBox4.Text = "資料庫";
                    int s1 = (int)Enum.Parse(typeof(SQL_Structure.StlMaterial), model.substoneMaterials[3]);
                    listBox4.SetSelected(s1, true);
                    status[3] = ModelStatus.exist;
                }

                if (model.substoneMaterials[4] == Enum.GetName(typeof(SQL_Structure.StlMaterial), SQL_Structure.StlMaterial.none))
                {
                    listBox5.SetSelected(0, true);
                    status[4] = ModelStatus.empty;
                }
                else
                {
                    textBox5.Text = "資料庫";
                    int s1 = (int)Enum.Parse(typeof(SQL_Structure.StlMaterial), model.substoneMaterials[4]);
                    listBox5.SetSelected(s1, true);
                    status[4] = ModelStatus.exist;
                }
            }
        }


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            isModified = false;
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (isModified)
            {
                if (ReadSelectedIndexs())
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    return;
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private bool ReadSelectedIndexs()
        {
            material[0] = Enum.GetName(typeof(SQL_Structure.StlMaterial), listBox1.SelectedIndex);
            material[1] = Enum.GetName(typeof(SQL_Structure.StlMaterial), listBox2.SelectedIndex);
            material[2] = Enum.GetName(typeof(SQL_Structure.StlMaterial), listBox3.SelectedIndex);
            material[3] = Enum.GetName(typeof(SQL_Structure.StlMaterial), listBox4.SelectedIndex);
            material[4] = Enum.GetName(typeof(SQL_Structure.StlMaterial), listBox5.SelectedIndex);
            

            if ((material[0] == "none" && status[0] != ModelStatus.empty) ||
                (material[1] == "none" && status[1] != ModelStatus.empty) ||
                (material[2] == "none" && status[2] != ModelStatus.empty) ||
                (material[3] == "none" && status[3] != ModelStatus.empty) ||
                (material[4] == "none" && status[4] != ModelStatus.empty))
            {
                MessageBox.Show("有檔案未選擇材質，請選擇^^");
                return false;
            }
            else if ((material[0] != "none" && status[0] == ModelStatus.empty) ||
                     (material[1] != "none" && status[1] == ModelStatus.empty) ||
                     (material[2] != "none" && status[2] == ModelStatus.empty) ||
                     (material[3] != "none" && status[3] == ModelStatus.empty) ||
                     (material[4] != "none" && status[4] == ModelStatus.empty))
            {
                MessageBox.Show("欄位有材質無檔案，請補齊^^");
                return false;
            }
            else if(CalculateNumOfSub()==false)
            {
                MessageBox.Show("請 依序 填滿模型，勿跳著填^^");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CalculateNumOfSub()
        {
            numofSubExist = 0;
            bool checkGapFlag = true; //Check if theres Gap between the stl seq
            for(int i =0; i < 5; i++)
            {
                if( status[i] != ModelStatus.empty && checkGapFlag==false)
                {//Gap exist!!!!!!!!!!
                    return false;
                }
                else if(status[i] == ModelStatus.empty)
                {
                    checkGapFlag = false; //Met first empty => flag revert
                }
                else
                {
                    numofSubExist += 1;
                }
            }
            return true;
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

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (Directory.Exists(default_folder_path))
            {
                openFileDialog1.InitialDirectory = default_folder_path;
                openFileDialog1.Filter = "Stl files| *.stl";
            }
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                Button b = (Button)sender;
                switch (b.Name)
                {
                    case "btn_browse1":
                        path[0] = openFileDialog1.FileName;
                        stl[0] = FileToByteArray(path[0]);
                        textBox1.Text = path[0];
                        status[0] = ModelStatus.exist;
                        isEachModified[0] = true;
                        break;

                    case "btn_browse2":
                        path[1] = openFileDialog1.FileName;
                        stl[1] = FileToByteArray(path[1]);
                        textBox2.Text = path[1];
                        status[1] = ModelStatus.exist;
                        isEachModified[1] = true;
                        break;

                    case "btn_browse3":
                        path[2] = openFileDialog1.FileName;
                        stl[2] = FileToByteArray(path[2]);
                        textBox3.Text = path[2];
                        status[2] = ModelStatus.exist;
                        isEachModified[2] = true;
                        break;

                    case "btn_browse4":
                        path[3] = openFileDialog1.FileName;
                        stl[3] = FileToByteArray(path[3]);
                        textBox4.Text = path[3];
                        status[3] = ModelStatus.exist;
                        isEachModified[3] = true;
                        break;

                    case "btn_browse5":
                        path[4] = openFileDialog1.FileName;
                        stl[4] = FileToByteArray(path[4]);
                        textBox5.Text = path[4];
                        status[4] = ModelStatus.exist;
                        isEachModified[4] = true;
                        break;

                    default:
                        MessageBox.Show("程式出錯了 ErrorA02");
                        break;
                }
                isModified = true;
            }
            openFileDialog1.Dispose();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            //check if originally all empty
            for (int i = 0; i < 5; i++)
            {
                if (status[i] != ModelStatus.empty)
                {
                    status[i] = ModelStatus.empty;
                    isEachModified[i] = true;
                    isModified = true;
                }
            }

            numofSubExist = 0;
            listBox1.SetSelected(0, true);
            listBox2.SetSelected(0, true);
            listBox3.SetSelected(0, true);
            listBox4.SetSelected(0, true);
            listBox5.SetSelected(0, true);
            for (int i = 0; i < 5; i++)
            {
                material[i] = Enum.GetName(typeof(SQL_Structure.StlMaterial), 0);
                stl[i] = null;
                path[i] = "";
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox l = (ListBox)sender;
            if (l.SelectedIndex == 0) //if selected "none" => clear path
            {
                switch (l.Name)
                {
                    case "listBox1":
                        textBox1.Text = "";
                        if (status[0] != ModelStatus.empty)
                        {
                            status[0] = ModelStatus.empty;
                            isEachModified[0] = true;
                            isModified = true;
                        }
                        //check if originally empty
                        break;
                    case "listBox2":
                        textBox2.Text = "";
                        if (status[1] != ModelStatus.empty)
                        {
                            status[1] = ModelStatus.empty;
                            isEachModified[1] = true;
                            isModified = true;
                        }
                        break;
                    case "listBox3":
                        textBox3.Text = "";
                        if (status[2] != ModelStatus.empty)
                        {
                            status[2] = ModelStatus.empty;
                            isEachModified[2] = true;
                            isModified = true;
                        }
                        break;
                    case "listBox4":
                        textBox4.Text = "";
                        if (status[3] != ModelStatus.empty)
                        {
                            status[3] = ModelStatus.empty;
                            isEachModified[3] = true;
                            isModified = true;
                        }
                        break;
                    case "listBox5":
                        textBox5.Text = "";
                        if (status[4] != ModelStatus.empty)
                        {
                            status[4] = ModelStatus.empty;
                            isEachModified[4] = true;
                            isModified = true;
                        }
                        break;
                    default:
                        MessageBox.Show("程式出錯了 ErrorF4A03");
                        break;

                }
            }
        }
    }
}
