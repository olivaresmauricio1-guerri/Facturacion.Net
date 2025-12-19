# Migration Documentation: frmNovecc (Novedades Cuenta Corriente)

## Overview
This document details the migration of the VB6 form `frmNovecc.frm` to VB.NET `frmNovecc.vb` within the Facturacion.Net project.

## Migration Details

### 1. Architecture
- **Pattern**: Singleton (GetInstance).
- **Data Access**: Uses `DataSourceManager.Lib` for SQL operations.
- **Mode Management**: Implemented `FormModoConsulta` and `FormModoEdicion` to manage control states (Enabled/Disabled/ReadOnly).

### 2. Controls Mapping
- **Grid**: Migrated from `MSDBGrid` (VB6) to `DataGridView` (VB.NET).
- **Combos**: Migrated to `System.Windows.Forms.ComboBox`. Loading logic centralized in `General.CargarCombos`.
- **Fields**: All textboxes and mask edits mapped to standard `TextBox` controls.
- **Checkbox**: `chkAnterior` migrated and integrated into logic.

### 3. Logic Implementation
- **Loading Data**:
  - `CargarNovedades`: Loads summary list into DataGridView.
  - `CargarDetalleNovedad`: Loads full record details when a row is selected or for editing.
- **Saving Data**:
  - `cmdAceptar_Click`: Handles both INSERT and UPDATE using parameterized SQL to prevent injection.
  - **PuntodeVenta Logic**: Implemented mapping based on `General.SucursalActual` (Suc.Tierra del Fuego -> 13, Casa Central -> 26, Default -> PuntoVentaActual).
- **Validation**:
  - `ValidarDatos`: Checks for required fields (Sucursal, Comprobante, etc.) and numeric formats (Monto, Bonificacion, Interno).
  - Special validation for Cheque number if TipoValor contains "CHEQUE".
- **UX**:
  - `Controls_KeyPress`: Implemented Enter-as-Tab functionality for better data entry flow.
  - Layout adjusted to prevent control overlapping.

### 4. Database Interaction
- **Table**: `NoveCtaCte`
- **Fields Handled**:
  - IdCtaCte, Fecha, NroComprobante, Importe, PuntodeVenta, IdImputacion, Observaciones
  - IdTipoVenta, IdCondicionVenta, IdSucursal, IdBanco, CodigoPostal, IdTipoValor
  - NroCheque, NroCupon, NroFactura, RegInterno, Bonificacion, ImpInterno, FechaVto, Anterior

### 5. Known Changes / Improvements
- **Parameterized Queries**: Replaced string concatenation with `SqlParameter`.
- **Error Handling**: Added Try-Catch blocks in critical methods.
- **UI Layout**: Adjusted control positions to accommodate .NET rendering differences.

## Usage
- Call `frmNovecc.GetInstance().Show()` to open the form.
