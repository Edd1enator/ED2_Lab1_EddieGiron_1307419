using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using Es_Arboles;
//using System.Text.Json;

namespace Lab01_EddieGiron_1307419
{
    public partial class Form1 : Form
    {
        private List<Persona> listPersona = new List<Persona>();
        private Es_Arboles.AVL<Persona> AVL = new Es_Arboles.AVL<Persona>(); 
        private List<Persona> listBusquedas = new List<Persona>();
        private int tlist;
        private int max;
        private int min;
        private int ncont;
        public Form1()
        {
            InitializeComponent();
        }
        public void CargarArchivo()
        {
            Persona aux = new Persona();

            string ruta = "C:/Users/eddie/OneDrive - Universidad Rafael Landivar/U/Año 5/Segundo Ciclo/Estructura de datos 2 (lab)/input.csv";
            
            foreach (string item in File.ReadLines(ruta))
            {
                if (item.StartsWith("INSERT;"))
                {
                    string json = item.Substring(7);
                    Persona persona = JsonConvert.DeserializeObject<Persona>(json);
                    listPersona.Add(persona);
                    AVL.Add(persona);
                }
                else if (item.StartsWith("DELETE;"))
                {
                    bool verifica = true;
                    string json = item.Substring(7);
                    Persona persona = JsonConvert.DeserializeObject<Persona>(json);
                    for (int i = 0; i < listPersona.Count; i++)
                    {
                        if (persona.name == listPersona[i].name && persona.dpi == listPersona[i].dpi)
                        {
                            listPersona.RemoveAt(i);
                            AVL.Remove(persona);
                            verifica = false;
                        }
                    }
                    if (verifica)
                    {
                        MessageBox.Show("Error, una persona a eliminar no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (item.StartsWith("PATCH;"))
                {
                    string json = item.Substring(6);
                    Persona persona = JsonConvert.DeserializeObject<Persona>(json);
                    for (int i = 0; i < listPersona.Count; i++)
                    {
                        if (persona.dpi == listPersona[i].dpi)
                        {
                            listPersona[i] = persona;
                        }
                    }
                }
            }
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            lstPersona.Items.Clear();
            tlist = listPersona.Count;
            max = tlist / 1000;
            min = 0;
            ncont = 0;
            if (tlist < 1000)
            {
                for (int i = 0; i < tlist; i++)
                {
                    lstPersona.Items.Add($"{listPersona[i].name} {listPersona[i].dpi} {listPersona[i].birthday} {listPersona[i].address}");
                }
            }
            else
            {
                for (int i = 0; i < 1000; i++)
                {
                    lstPersona.Items.Add($"{listPersona[i].name} {listPersona[i].dpi} {listPersona[i].birthday} {listPersona[i].address}");
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Persona> prueba = new List<Persona>();
            lstPersona.Items.Clear();
            if (txtBuscar.Text.Length == 0)
            {
                MessageBox.Show("No se digitó ningún nombre", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            string nombre = txtBuscar.Text;
            Persona Buscar = new Persona();
            Buscar.name = nombre;
            Node<Persona> BuscarN = new Node<Persona>();
            BuscarN.Valor = Buscar;
            //prueba = AVL.DevuelveBusqueda(BuscarN, );
            foreach (Persona item in listPersona)
            {
                if (item.name == nombre)
                {
                    lstPersona.Items.Add($"{item.name} {item.dpi} {item.birthday} {item.address}");
                }
            }
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            lstPersona.Items.Clear();
            if (ncont <= max)
            {
                ncont++;
                int indice = ncont * 1000;
                if (indice+1000 > tlist)
                {
                    for (int i = indice; i < tlist; i++)
                    {
                        lstPersona.Items.Add($"{listPersona[i].name} {listPersona[i].dpi} {listPersona[i].birthday} {listPersona[i].address}");
                    }
                }
                else
                {
                    for (int i = indice; i < indice + 1000; i++)
                    {
                        lstPersona.Items.Add($"{listPersona[i].name} {listPersona[i].dpi} {listPersona[i].birthday} {listPersona[i].address}");
                    }
                }
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            lstPersona.Items.Clear();
            if (ncont > min)
            {
                ncont--;
                int indice = ncont * 1000;
                if (indice + 1000 > tlist)
                {
                    indice = tlist - 1000;
                    for (int i = indice; i < tlist; i++)
                    {
                        lstPersona.Items.Add($"{listPersona[i].name} {listPersona[i].dpi} {listPersona[i].birthday} {listPersona[i].address}");
                    }
                }
                else
                {
                    for (int i = indice; i < indice + 1000; i++)
                    {
                        lstPersona.Items.Add($"{listPersona[i].name} {listPersona[i].dpi} {listPersona[i].birthday} {listPersona[i].address}");
                    }
                }
            }
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            string rutasalida = "C:/Users/eddie/OneDrive - Universidad Rafael Landivar/U/Año 5/Segundo Ciclo/Estructura de datos 2 (lab)/output.json";
            string jsonout = JsonConvert.SerializeObject(listPersona, Formatting.Indented);
            File.WriteAllText(rutasalida, jsonout);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CargarArchivo();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
