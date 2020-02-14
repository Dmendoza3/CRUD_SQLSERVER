Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class PersonasModel
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=PersonasModel")
    End Sub

    Public Overridable Property Personas As DbSet(Of Personas)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of Personas)() _
            .Property(Function(e) e.Identidad) _
            .IsFixedLength()

        modelBuilder.Entity(Of Personas)() _
            .Property(Function(e) e.Nombre) _
            .IsFixedLength()

        modelBuilder.Entity(Of Personas)() _
            .Property(Function(e) e.Telefono) _
            .IsFixedLength()

        modelBuilder.Entity(Of Personas)() _
            .Property(Function(e) e.Cargo) _
            .IsFixedLength()
    End Sub
End Class
