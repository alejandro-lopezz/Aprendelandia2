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
    public partial class Form1 : Form
    {
        //Le agrego un comentario... actualmente estoy en mi rama desarrollo...
        private List<Disco> listaDiscos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DiscoNegocio negocio = new DiscoNegocio();
                listaDiscos = negocio.listar();
                dgvDiscos.DataSource = listaDiscos;
                ocultarColumnas();
                pbxImagenAlbum.Load(listaDiscos[0].UrlImagen);

                //cargo cbo de filtro avanzado
                cboCampo.Items.Add("Cantidad de Canciones");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dgvDiscos.Columns["UrlImagen"].Visible = false;
            dgvDiscos.Columns["IdEstilo"].Visible = false;
            dgvDiscos.Columns["IdTipoEdicion"].Visible = false;
            dgvDiscos.Columns["Id"].Visible = false;
        }

        private void actualizarGrilla()
        {
            DiscoNegocio negocio = new DiscoNegocio();
            listaDiscos = negocio.listar();
            dgvDiscos.DataSource = listaDiscos;
            dgvDiscos.Columns["UrlImagen"].Visible = false;
            pbxImagenAlbum.Load(listaDiscos[0].UrlImagen);
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagenAlbum.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagenAlbum.Load("https://i0.wp.com/stretchingmexico.com/wp-content/uploads/2024/07/placeholder.webp?fit=1200%2C800&quality=80&ssl=1");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            formAgregar agregar = new formAgregar();
            agregar.ShowDialog();
            actualizarGrilla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;

            formAgregar modificar = new formAgregar(seleccionado);
            modificar.ShowDialog();
            actualizarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            try
            {
                DialogResult resultado = MessageBox.Show("¿Estas seguro de querer elimnarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    MessageBox.Show("Eliminado correctamente");
                    actualizarGrilla();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Disco> listaFiltrada;
            string filtro = txtFiltroRapido.Text;

            if (filtro != "")
            {
                listaFiltrada = listaDiscos.FindAll(x => x.Titulo.ToLower().Contains(filtro.ToLower()) || x.Estilo.Descripcion.ToLower().Contains(filtro.ToLower()) || x.Tipo.Descripcion.ToLower().Contains(filtro.ToLower()));
                dgvDiscos.DataSource = listaFiltrada;
            }
            else
            {
                dgvDiscos.DataSource = listaDiscos;
            }
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campo = cboCampo.SelectedItem.ToString();

            if (campo == "Cantidad de Canciones")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
        }

        private bool validarFiltroAvanzado()
        {
            if (cboCampo.SelectedIndex == -1)
            {
                MessageBox.Show("Ingrese un valor en el campo para filtar");
                return true;
            }
            else if (cboCriterio.SelectedIndex == -1)
            {
                MessageBox.Show("Ingrese un valor en el criterio para filtar");
                return true;
            }
            else if (cboCampo.SelectedItem.ToString() == "Cantidad de Canciones")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debes cargar un valor en el filtro");
                    return true;
                }
                {
                }
                if (!(soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Ingrese solo números por favor");
                    return true;
                }
            }

            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }

            return true;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DiscoNegocio aux = new DiscoNegocio();
            try
            {
                if (validarFiltroAvanzado() == true)
                    return;

                string campo = cboCampo.Text;
                string criterio = cboCriterio.Text;
                string filtro = txtFiltroAvanzado.Text;

                dgvDiscos.DataSource = aux.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
