Imports System.Data.SqlClient

Public Class Form1
    Dim context As CRUD_SQLSERVER.PersonasModel = New PersonasModel

    Dim connection As New SqlConnection("Server=localhost\SQLEXPRESS;Database=CRUDDB;Trusted_Connection=True;")
    Dim cmd As SqlCommand

    'Data
    Public dbda As SqlDataAdapter
    Public dbdt As DataTable

    Public params As New List(Of SqlParameter)

    Public recordCount As Integer
    Public Exception As String

    Public Sub ExecQuery(Query As String)
        recordCount = 0
        Exception = ""
        Try
            connection.Open()

            cmd = New SqlCommand(Query, connection)

            params.ForEach(Sub(p) cmd.Parameters.Add(p))

            params.Clear()

            dbdt = New DataTable
            dbda = New SqlDataAdapter(cmd)
            recordCount = dbda.Fill(dbdt)
        Catch ex As Exception
            Exception = "Error: " & vbCrLf & ex.Message
        Finally

            If connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Sub

    Public Sub ShowTable()
    End Sub

    Public Sub EmptyTextboxes()
        txtNombre.Text = ""
        txtIdentidad.Text = ""
        txtTelefono.Text = ""
        txtCargo.Text = ""
    End Sub

    Public Sub addParam(Name As String, Value As Object)
        Dim newParam As New SqlParameter(Name, Value)
        params.Add(newParam)
    End Sub

    Public Function HasException(Optional Report As Boolean = False) As Boolean
        If String.IsNullOrEmpty(Exception) Then Return False
        If Report = True Then
            If InStr(Exception, "Violation of PRIMARY KEY constraint") Then
                Exception = "Un registro con esa identidad ya existe. No se creo el registro."
            End If
            MsgBox(Exception, MsgBoxStyle.Critical, "Exception:")
        End If
        Return True
    End Function

    Private Sub btn_nuevo_Click(sender As Object, e As EventArgs) Handles btn_nuevo.Click
        Dim temp As Personas = New Personas
        temp.Nombre = txtNombre.Text
        temp.Identidad = txtIdentidad.Text
        temp.Telefono = txtTelefono.Text
        temp.Cargo = txtCargo.Text

        context.Personas.Add(temp)

        context.SaveChanges()
        EmptyTextboxes()
        ShowTable()
    End Sub

    Private Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        Dim nombre As String = txtNombre.Text
        Dim identidad As String = txtIdentidad.Text
        Dim telefono As String = txtTelefono.Text
        Dim cargo As String = txtCargo.Text

        Dim query = From it In context.Personas
                    Where it.Identidad = identidad
                    Select it

        query.First().Nombre = nombre
        query.First().Telefono = telefono
        query.First().Cargo = cargo

        context.SaveChanges()

        EmptyTextboxes()
        ShowTable()
    End Sub

    Private Sub btn_eliminar_Click(sender As Object, e As EventArgs) Handles btn_eliminar.Click
        Dim identidad As String = txtIdentidad.Text

        Dim query = From it In context.Personas
                    Where it.Identidad = identidad
                    Select it

        context.Personas.Remove(query.First())
        context.SaveChanges()
        EmptyTextboxes()
        ShowTable()
    End Sub

    Private Sub btn_mostrar_Click(sender As Object, e As EventArgs) Handles btn_mostrar.Click
        EmptyTextboxes()
        ShowTable()
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ShowTable()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txtIdentidad.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtNombre.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtTelefono.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtCargo.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
    End Sub
End Class
