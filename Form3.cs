﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMP_cs
{
    public partial class Form3 : Form
    {
        string sqlQuery = "";
        DB_connect dB_Connect;
        DataTable dB_dt;
        Form2 frm2;

        public Form3(Form2 frm2)
        {
            InitializeComponent();
            this.MaximizeBox = false; // 전체화면 비활성화
            this.frm2 = frm2;
        }


        private void button1_Click(object sender, EventArgs e) // 등록 버튼
        {
            dB_Connect = new DB_connect();
            dB_Connect.Open();
            
            dB_dt= dB_Connect.Copy_DT(dB_dt, "Items");

            string Check = $"{textBox1.Text}";
            bool contains = dB_dt.AsEnumerable().Any(row => Check == row.Field<String>("Name"));

            if (contains == false)
            {
                sqlQuery = $"INSERT INTO Items values('{textBox4.Text}','{textBox1.Text}',{textBox3.Text},{textBox2.Text});";
                dB_Connect.SQLQuery(sqlQuery);
            }
            else
            {
                MessageBox.Show("기존에 존재하는 물품 입니다!\n해당 제품은 제품 정보 변경 기능을 이용하세요!", "알림창", MessageBoxButtons.OK);
            }


            frm2.Update_DB();

            dB_Connect.Close();
            this.Close();

            // 텍스트 박스에 담긴 값 DB로 전송

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 수량의 경우 숫자가 아니면 입력 불가
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 수량의 경우 숫자가 아니면 입력 불가
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
    }
}
