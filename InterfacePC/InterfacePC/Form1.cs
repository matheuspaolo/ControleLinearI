﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace InterfacePC
{
    public partial class Form1 : Form
    {

        double valor1, valor2, valor3;
        string direcao;

        public Form1()
        {
            InitializeComponent();
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente; //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (cBoxCOMs.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (cBoxCOMs.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }

            //limpa comboBox
            cBoxCOMs.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                cBoxCOMs.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            //cBoxCOMs.SelectedIndex = 0;
        }

        private void timerCOM_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (SerialPort.IsOpen == false)
            {
                try
                {
                    SerialPort.PortName = cBoxCOMs.Items[cBoxCOMs.SelectedIndex].ToString();
                    SerialPort.Open();

                }
                catch
                {
                    return;

                }
                if (SerialPort.IsOpen)
                {
                    btnConectar.Text = "Desconectar";
                    cBoxCOMs.Enabled = false;

                }
            }
            else
            {

                try
                {
                    SerialPort.Close();
                    cBoxCOMs.Enabled = true;
                    btnConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }

            }
        }

        private void calculaValores()
        {
            try
            {
                switch (cBoxPeso1.SelectedIndex)
                {
                    case 0:
                        valor1 = Convert.ToDouble(txtBoxValor1.Text);
                        break;
                    case 1:
                        valor1 = Convert.ToDouble(txtBoxValor1.Text) * (1000);
                        break;
                    case 2:
                        valor1 = Convert.ToDouble(txtBoxValor1.Text) * (1000000);
                        break;

                }
                switch (cBoxPeso2.SelectedIndex)
                {
                    case 0:
                        valor2 = Convert.ToDouble(txtBoxValor2.Text);
                        break;
                    case 1:
                        valor2 = Convert.ToDouble(txtBoxValor2.Text) * (1000);
                        break;
                    case 2:
                        valor2 = Convert.ToDouble(txtBoxValor2.Text) * (1000000);
                        break;

                }
                switch (cBoxPeso3.SelectedIndex)
                {
                    case 0:
                        valor3 = Convert.ToDouble(txtBoxValor3.Text);
                        break;
                    case 1:
                        valor3 = Convert.ToDouble(txtBoxValor3.Text) * (1000);
                        break;
                    case 2:
                        valor3 = Convert.ToDouble(txtBoxValor3.Text) * (1000000);
                        break;

                }
            }
            catch
            {
                DialogResult res = MessageBox.Show("Tem erro aê meu parsero... \narruma as resistências direitinho amigão", "Ih rapaz...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ativarMotor()
        {

            string velocidade = Convert.ToString(trackBarVelocidade.Value);

            if (rBtnHorario.Checked)
            {
                direcao = "horario";
            }

            if (rBtnAntiHorario.Checked)
            {
                direcao = "antihorario";
            }

            if (SerialPort.IsOpen)
            {
                SerialPort.WriteLine("velocidade");
                SerialPort.WriteLine(velocidade);
                SerialPort.WriteLine("direcao");
                SerialPort.WriteLine(direcao);
            }
        }

        private void btnConectar_Click_1(object sender, EventArgs e)
        {
            if (SerialPort.IsOpen == false)
            {
                try
                {
                    SerialPort.PortName = cBoxCOMs.Items[cBoxCOMs.SelectedIndex].ToString();
                    SerialPort.Open();

                }
                catch
                {
                    return;

                }
                if (SerialPort.IsOpen)
                {
                    btnConectar.Text = "Desconectar";
                    cBoxCOMs.Enabled = false;

                }
            }
            else
            {

                try
                {
                    SerialPort.Close();
                    cBoxCOMs.Enabled = true;
                    btnConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }

            }
        }

        private void desativarMotor()
        {
            SerialPort.WriteLine("desligar");
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (btnIniciar.Text == "Iniciar")
            {
                calculaValores();
                ativarMotor();

                SerialPort.WriteLine("valor1");
                SerialPort.WriteLine(Convert.ToString(valor1));

                SerialPort.WriteLine("valor2");
                SerialPort.WriteLine(Convert.ToString(valor2));

                SerialPort.WriteLine("valor3");
                SerialPort.WriteLine(Convert.ToString(valor3));
            }
        }
    }
}