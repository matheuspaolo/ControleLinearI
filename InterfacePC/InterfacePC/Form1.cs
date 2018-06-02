using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace InterfacePC
{
    public partial class Form1 : Form
    {

        double valor1, valor2, valor3;
        string direcao, RxString;

        public Form1()
        {
            InitializeComponent();
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

            btnConectar.Enabled = false;
            btnDesconectar.Enabled = true;
        }

        private void desativarMotor()
        {
            SerialPort.WriteLine("desligar");
        }

        private void btnDesliga_Click(object sender, EventArgs e)
        {
            SerialPort.Write("desliga,");
            SerialPort.Write("aylimao");
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            SerialPort.Close();
            btnConectar.Enabled = true;
            btnDesconectar.Enabled = false;
            cBoxCOMs.Enabled = true;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBoxValor1.Text = "";
            txtBoxValor2.Text = "";
            txtBoxValor3.Text = "";
            cBoxCOMs.SelectedIndex = -1;
            cBoxPeso1.SelectedIndex = -1;
            cBoxPeso2.SelectedIndex = -1;
            cBoxPeso3.SelectedIndex = -1;
            txtBoxRRecebida.Text = "";
            txtBoxSerialRx.Text = "";
            btnDesconectar.Enabled = false;
            btnConectar.Enabled = true;

        }


        private void btnIniciar_Click(object sender, EventArgs e)
        {
            calculaValores();
            string send1 = String.Format(Convert.ToString(valor1));
            string send2 = String.Format(Convert.ToString(valor2));
            string send3 = String.Format(Convert.ToString(valor3));
            SerialPort.Write("receberv1,");
            SerialPort.Write(send1);

            Thread.Sleep(1500);

            SerialPort.Write("receberv2,");
            SerialPort.Write(send2);

            Thread.Sleep(1500);
            SerialPort.Write("receberv3,");
            SerialPort.Write(send3);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RxString = SerialPort.ReadExisting();              //le o dado disponível na serial
            this.Invoke(new EventHandler(trataDadoRecebido));   //chama outra thread para escrever o dado no text box
        }

        private void trataDadoRecebido(object sender, EventArgs e)
        {
            txtBoxSerialRx.AppendText(RxString);

        }

    }

}