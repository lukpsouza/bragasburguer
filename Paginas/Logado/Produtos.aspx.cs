﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Paginas_Logado_Produtos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarRepeat();
        }

    }

    private void CarregarRepeat()
    {
        DataSet ds = ProdutosBD.SelecionarTodos();
        rpt.DataSource = ds;
        rpt.DataBind();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Button btn = (sender as Button);
        string[] arr = btn.CommandArgument.ToString().Split('|');

        txtCodigo.Text = arr[0];
        txtNome.Text = arr[1];
        txtDescricao.Text = arr[2];
        txtValor.Text = arr[3];


        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#ModalEditar').modal('show');</script>", false);
    }

    protected void btnEditModal_Click(object sender, EventArgs e)
    {
        Produtos p = new Produtos();
        p.Codigo = Convert.ToInt32(txtCodigo.Text);
        p.Nome = txtNome.Text;
        p.Descricao = txtDescricao.Text;
        p.Valor = Convert.ToDouble(txtValor.Text);

        switch (ProdutosBD.Update(p))
        {
            case 0:
                ltlMSG.Text = "<p class='text-success'> Cadastro editado com sucesso! </p>";
                CarregarRepeat();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#modalMSG').modal('show');</script>", false);
                break;
            case -2:
                ltlMSG.Text = "<p class='text-danger'> Não foi possível editar o registro!</p>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#modalMSG').modal('show');</script>", false);
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button btn = (sender as Button);
        string[] arr = btn.CommandArgument.ToString().Split('|');

        ltlDeletar.Text = "<p class='text-danger'>Deseja realmente deletar o registro:<br/>";
        ltlDeletar.Text += "Código: " + arr[0] + " Nome: " + arr[1] + "</p>";
        ltlCodigo.Text = arr[0];
        ltlNome.Text = arr[1];

        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#ModalDeletar').modal('show');</script>", false);
    }

    protected void btnDeleteModal_Click(object sender, EventArgs e)
    {

        switch (ProdutosBD.Delete(Convert.ToInt32(ltlCodigo.Text)))
        {
            case 0:
                ltlMSG.Text = "<p class='text-success'> Registro deletado com sucesso! </p>";
                CarregarRepeat();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#modalMSG').modal('show');</script>", false);
                break;
            case -2:
                ltlMSG.Text = "<p class='text-danger'> Não foi possível Deletar o registro!</p>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#modalMSG').modal('show');</script>", false);
                break;
        }

    }
}