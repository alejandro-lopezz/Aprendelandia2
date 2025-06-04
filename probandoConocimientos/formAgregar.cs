using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace probandoConocimientos
{
    public partial class formAgregar : Form
    {
        private Disco disco = null;
        public formAgregar()
        {
            InitializeComponent();
        }

        public formAgregar(Disco disco)
        {
            InitializeComponent();
            this.disco = disco;
            Text = "Modificar";
            toolTip1.Active = false;
            //le agrego este comentario desde mi rama desarrollo para hacer el merge en mi main despues...
        }

        private bool soloLetras(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsLetter(caracter)))
                {
                    return false;
                }
            }

            return true;
        }
        private bool validarFiltro()
        {
            string titulo = txtTitulo.Text;
            string urlImagen = txtUrlImagen.Text;

            if (titulo == "")
            {
                MessageBox.Show("Porfavor ingrese un valor en el Titulo");
                return false;
            }
            if (titulo != "")
            {
                if (soloLetras(titulo) == false)
                {
                    MessageBox.Show("Solo se admiten Letras en este campo");
                    return false;
                }
            }
            if (urlImagen == "")
            {
                MessageBox.Show("Porfavor ingrese un valor en la UrlImagen");
                return false;
            }

            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarFiltro() == false)
                return;

            //Disco aux = new Disco();
            DiscoNegocio negocio = new DiscoNegocio();
            try
            {
                if (disco == null)
                {
                    disco = new Disco();
                }

                disco.Titulo = txtTitulo.Text;
                disco.FechaLanzamiento = dtpFechaLanzamiento.Value;
                disco.CantidadCanciones = (int)numCantidadCanciones.Value;
                disco.UrlImagen = txtUrlImagen.Text;
                disco.Estilo = (Estilo)cboEstilo.SelectedItem;
                disco.Tipo = (Tipo)cboTipo.SelectedItem;

                if (disco.Id != 0)
                {
                    negocio.modificar(disco);
                    MessageBox.Show("Modificado exitosamente!!");
                    Close();
                }
                else
                {
                    negocio.agregar(disco);
                    MessageBox.Show("Agregado exitosamente!!");
                    Close();
                }
    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxAgregar.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxAgregar.Load("https://i0.wp.com/stretchingmexico.com/wp-content/uploads/2024/07/placeholder.webp?fit=1200%2C800&quality=80&ssl=1");
            }
        }

        private void btnCanelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formAgregar_Load(object sender, EventArgs e)
        {
            EstiloNegocio negocio = new EstiloNegocio();
            TipoNegocio tipoNegocio = new TipoNegocio();
            try
            {
                cboEstilo.DataSource = negocio.listar();
                cboEstilo.ValueMember = "Id";
                cboEstilo.DisplayMember = "Descripcion";
                cboTipo.DataSource = tipoNegocio.listar();
                cboTipo.ValueMember = "Id";
                cboTipo.DisplayMember = "Descripcion";

                if (disco != null)
                {
                    txtTitulo.Text = disco.Titulo;
                    dtpFechaLanzamiento.Text = disco.FechaLanzamiento.ToString();
                    numCantidadCanciones.Value = disco.CantidadCanciones;
                    txtUrlImagen.Text = disco.UrlImagen;
                    cboEstilo.SelectedValue = disco.IdEstilo;
                    cboTipo.SelectedValue = disco.IdTipoEdicion;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
