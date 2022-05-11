﻿Imports Solmicro.Expertis
Imports Solmicro.Expertis.Engine.BE.BusinessProcesses
Imports Solmicro.Expertis.Engine.DAL

Public Class Contratos

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbVehiculoContrato"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf FillDefaultValues)
    End Sub

    <Task()> Public Shared Sub FillDefaultValues(ByVal data As DataRow, ByVal services As ServiceProvider)
        ProcessServer.ExecuteTask(Of DataRow)(AddressOf AsignarValoresPredeterminados, data, services)
        'ProcessServer.ExecuteTask(Of DataRow)(AddressOf AsignarCentroGestion, data, services)
        'ProcessServer.ExecuteTask(Of DataRow)(AddressOf AsignarContador, data, services)
    End Sub

    <Task()> Public Shared Sub AsignarValoresPredeterminados(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("IDContrato") = AdminData.GetAutonumeric
        'Dim dt As New DataTable
        'dt = AdminData.GetData("SELECT MAX(IDContrato) As ID FROM tbVehiculoContrato")

        'data("IDContrato") = dt(0)(0) + 1
    End Sub

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarValoresPredeterminados)
    End Sub

    Public Sub EjecutarSql(ByVal sql As String)

        AdminData.Execute(sql)

    End Sub

    Function EjecutarSqlSelect(ByVal sql As String)

        Dim cadena As DataTable
        cadena = AdminData.GetData(sql)
        Return cadena

    End Function

    Function recuperarContrato(ByVal idvehiculo As String)

        Dim filter As New Engine.Filter
        'filter.Add("idvehiculo", Engine.FilterOperator.Equal, idvehiculo)
        'filter.Add("FFin", Engine.FilterOperator.LessThan, FFin)
        Dim dtContr As New DataTable
        Dim contrato As New Contratos
        dtContr = contrato.Filter("*", "IdVehiculo='" & idvehiculo & "' and (FFin<Finicio or FFin is null) ", )

        If dtContr.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
