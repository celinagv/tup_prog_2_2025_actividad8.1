using Ejercicio1.Models;
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

namespace Ejercicio1
{
    public partial class Form1 : Form
    {
        List<Cuenta> cuentas = new List<Cuenta>();
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string n = tbNombre.Text;
            int dni = Convert.ToInt32(tbDni.Text);
            double i = Convert.ToDouble(tbImporte.Text);
            Cuenta nueva = new Cuenta(dni, n, i);

            cuentas.Sort();
            int idx = cuentas.BinarySearch(nueva);

            if (idx > 0)
            {
                cuentas[idx].Nombre = nueva.Nombre;

                cuentas[idx].Importe += nueva.Importe;
            }
            else
                cuentas.Add(nueva);

            btnActualizar.PerformClick();

            }
        
            

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            lsbVer.Items.Clear();
            foreach (Cuenta p in cuentas)
            {
                lsbVer.Items.Add(p);
            }
        }

       
        private void lsbVer_SelectedValueChanged(object sender, EventArgs e)
        {
            Cuenta seleccionado = lsbVer.SelectedItem as Cuenta;
            if(seleccionado != null)
            {
                tbDni.Text = seleccionado.Dni.ToString("00.000.000");
                tbNombre.Text = seleccionado.Nombre;
                tbImporte.Text = seleccionado.Importe.ToString("0.00");

            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream archivo = null;
                StreamReader sr = null;
                string path = openFileDialog1.FileName;

                try
                {
                    archivo = new FileStream(path, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(archivo);

                    while (!sr.EndOfStream)// sr.EndOffStream == false
                    {
                        string renglon = sr.ReadLine();
                        string dni = renglon.Substring(0,9).Trim();
                        string nombre = renglon.Substring(9, 10).Trim();
                        string importe = renglon.Substring(19, 9).Trim();

                        Cuenta nueva = new Cuenta(Convert.ToInt32(dni), nombre, Convert.ToDouble(importe));

                        cuentas.Add(nueva);
                    }
                }
              
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    if(sr!=null) sr.Close();
                    if (archivo != null) archivo.Close(); 
                }
              
            }
            btnActualizar.PerformClick();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()== DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                FileStream archivo = null;
                StreamWriter sw = null;

                try
                {
                    archivo = new FileStream(path, FileMode.Open, FileAccess.Write);
                    sw = new StreamWriter(archivo);

                    foreach (Cuenta p in cuentas)
                    {
                        string nombre = p.Nombre;
                        if (p.Nombre.Length > 10)
                        {
                            nombre = nombre.Substring(0, 10);
                        }
                        string renglon = $"{p.Dni,+9}{p.Nombre,-10}{p.Importe,+9}";
                        sw.WriteLine(renglon);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                finally
                {
                    if (sw != null) sw.Close();
                    if (archivo != null) archivo.Close();
                }
            }
        }
    }
}
