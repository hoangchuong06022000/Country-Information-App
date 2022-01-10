using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.Model;
using Newtonsoft.Json;
using System.Threading;

namespace Client
{
    public partial class GUI_ListCountry : Form
    {
        public static SimpleTcpClient client;
        private string jsonString;
        private GEOSNAME api;
        public GUI_ListCountry()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.Connect("127.0.0.1", 2000);
            client.StringEncoder = Encoding.UTF8;
            client.WriteLineAndGetReply("connect", TimeSpan.FromSeconds(0));
            client.DataReceived += Receive;
            Thread.Sleep(4000);
            listView1.Clear();
            listView1.Columns.Add("Tên quốc gia", 250);
            this.api = this.ConvertJSONToObject();
            if (this.api != null)
            {
                foreach (var item in this.api.geonames)
                {
                    ListViewItem listitem = new ListViewItem(item.countryName);
                    listitem.SubItems.Add(item.capital);
                    listitem.SubItems.Add(item.languages);
                    listitem.SubItems.Add(item.continentName);
                    listitem.SubItems.Add(item.currencyCode);
                    listitem.SubItems.Add(item.areaInSqKm);
                    listitem.SubItems.Add(item.population);
                    listitem.Tag = item.countryName;
                    this.listView1.Items.Add(listitem);
                }
            }
        }

        public GEOSNAME ConvertJSONToObject()
        {
            GEOSNAME countries = JsonConvert.DeserializeObject<GEOSNAME>(this.jsonString);
            if ((countries == null | countries.geonames == null))
            {
                return null;
            }
            else
            {
                return countries;
            }
        }

        private void Receive(object sender, SimpleTCP.Message e)
        {
            string tmp = e.MessageString;
            this.jsonString = tmp.Remove(tmp.Length - 1, 1);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            listView1.Clear();
            listView1.Columns.Add("Tên quốc gia", 250);
            this.api = this.ConvertJSONToObject();
            if (this.api != null)
            {
                foreach (var item in this.api.geonames)
                {
                    String mutiple_text = item.countryName + item.languages;
                    if (mutiple_text.Contains(txtFilter.Text))
                    {
                        ListViewItem listitem = new ListViewItem(item.countryName);
                        listitem.SubItems.Add(item.capital);
                        listitem.SubItems.Add(item.languages);
                        listitem.SubItems.Add(item.continentName);
                        listitem.SubItems.Add(item.currencyCode);
                        listitem.SubItems.Add(item.areaInSqKm);
                        listitem.SubItems.Add(item.population);
                        listitem.Tag = item.countryName;
                        this.listView1.Items.Add(listitem);
                    }
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GUI_CountryInfo gui_CountryInfo = new GUI_CountryInfo();
            gui_CountryInfo.txtThuDo.Text = listView1.SelectedItems[0].SubItems[1].Text;
            gui_CountryInfo.txtNgonNgu.Text = listView1.SelectedItems[0].SubItems[2].Text;
            gui_CountryInfo.txtChauLuc.Text = listView1.SelectedItems[0].SubItems[3].Text;
            gui_CountryInfo.txtTienTe.Text = listView1.SelectedItems[0].SubItems[4].Text;
            gui_CountryInfo.txtDienTich.Text = listView1.SelectedItems[0].SubItems[5].Text + " sq-km";
            gui_CountryInfo.txtDanSo.Text = listView1.SelectedItems[0].SubItems[6].Text + " người";
            gui_CountryInfo.ShowDialog();
        }
    }
}
