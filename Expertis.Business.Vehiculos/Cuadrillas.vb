Imports Solmicro.Expertis
Imports Solmicro.Expertis.Engine.BE.BusinessProcesses
Imports Solmicro.Expertis.Engine.DAL
Imports Solmicro.Expertis.Business.General

Public Class Cuadrillas

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbVehiculoCuadrilla"

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
        'If (data(0)("idCuadrilla") Is Nothing) Then
        data("idCuadrilla") = AdminData.GetAutoNumeric
        'End If
    End Sub

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        'updateProcess.AddTask(Of DataRow)(AddressOf AsignarValoresPredeterminados)

    End Sub

    Public Sub EjecutarSql(ByVal sql As String)

        AdminData.Execute(sql)

    End Sub

    Function EjecutarSqlSelect(ByVal sql As String)

        Dim cadena As DataTable
        cadena = AdminData.GetData(sql)
        Return cadena

    End Function

    Function recuperarCuadrilla(ByVal idvehiculo As String)

        Dim filter As New Engine.Filter
        'filter.Add("idvehiculo", Engine.FilterOperator.Equal, idvehiculo)
        'filter.Add("FFin", Engine.FilterOperator.LessThan, FFin)
        Dim dtCuad As New DataTable
        Dim cuadrilla As New Cuadrillas
        dtCuad = cuadrilla.Filter("*", "IdVehiculo='" & idvehiculo & "' and (FFin<Finicio or FFin is null) ", )

        If dtCuad.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


End Class
