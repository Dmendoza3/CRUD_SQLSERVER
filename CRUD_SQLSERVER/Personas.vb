Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class Personas
    <Key>
    <StringLength(15)>
    Public Property Identidad As String

    <StringLength(20)>
    Public Property Nombre As String

    <StringLength(9)>
    Public Property Telefono As String

    <StringLength(12)>
    Public Property Cargo As String
End Class
