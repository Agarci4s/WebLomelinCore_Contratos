using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebLomelinCore.Models;

namespace WebColliersCore.Models
{
    public class CgPropietarios
    {
        #region  Datos generales del propietario

        public Int64 IdPropietario { get; set; }


        [Display(Name = "Nombre")]
        [MaxLength(50)]
        public string Nombres { get; set; }
        
        [Display(Name = "Apellido Paterno")]
        [MaxLength(50)]
        public string Paterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [MaxLength(50)]
        public string Materno { get; set; }

        [Display(Name = "Antenombre")]
        [MaxLength(25)]
        public string AnteNombre { get; set; }

        [Display(Name = "Curp")]
        [MaxLength(18, ErrorMessage ="Longitud máxima de 18 caracteres.")]
        [RegularExpression("[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}" +
                            "(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])" +
                            "[HM]{1}" +
                            "(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)" +
                            "[B-DF-HJ-NP-TV-Z]{3}" +
                            "[0-9A-Z]{1}[0-9]{1}$", ErrorMessage ="Formato inválido.")]
        public string CURP { get; set; }

        [Display(Name = "Persona(Física/Moral)")]
        [MaxLength(50)]
        public string Fis_Mor { get; set; }

        [Required(ErrorMessage = "Campo RFC requerido")]
        [Display(Name = "RFC")]
        [MinLength(12, ErrorMessage = "Longitud mínima de 12 caracteres.")]
        [MaxLength(13, ErrorMessage = "Longitud máxima de 13 caracteres.")]
       
        public string RFC { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(220)]
        public string Nombre { get; set; }

        public string TipIdentMoral { get; set; }

        public string NumIdentMoral { get; set; }

        public string TipIdentFisica { get; set; }

        public string NumIdentFisica { get; set; }

        [Display(Name = "Tipo de identificación")]
        [MaxLength(2)]
        public string TipoIdentificacionAux { get; set; }

        [Display(Name = "Número de identificación")]
        [MaxLength(40)]
        public string NumeroIdentificacionAux { get; set; }

        [Display(Name = "País de nacimiento")]
        public int Nacionalidad { get; set; }

        [Display(Name = "Segundo propietario")]
        [MaxLength(100)]
        public string PropietarioDos { get; set; }

        #endregion

        #region Domicilio propietario 

        [Display(Name = "Calle")]
        [MaxLength(60)]
        public string Domicilio { get; set; }

        [Display(Name = "Número Exterior")]
        [MaxLength(50)]
        public string Exterior { get; set; }

        [Display(Name = "Número Interior")]
        [MaxLength(50)]
        public string Interior { get; set; }

        [Display(Name = "Colonia")]
        [MaxLength(60)]
        public string Colonia { get; set; }

        [Display(Name = "Alcaldía o municipio")]
        [MaxLength(50)]
        public string Delegacion { get; set; }

        [Display(Name = "Código Postal")]
        [MaxLength(5)]
        public string Cp { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(50)]
        public string Ciudad { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(30)]
        public string Estado { get; set; }

        [Display(Name = "País")]
        [MaxLength(60)]
        public string Pais { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(10)]
        [Phone(ErrorMessage = "Formato telefónico incorrecto")]        
        public string Telefono1 { get; set; }

        [Display(Name = "Teléfono 2")]
        [MaxLength(10)]
        [Phone(ErrorMessage = "Formato telefónico incorrecto")]
        public string Telefono2 { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(10)]
        [Phone(ErrorMessage = "Formato telefónico incorrecto")]
        public string Celular { get; set; }

        [Display(Name = "Fax")]
        [MaxLength(20)]
        public string FAX { get; set; }

        [Display(Name = "E-mail")]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage ="Formato inválido.")]
        public string EMAIL { get; set; }

        #endregion

        #region Domicilio fiscal
        [Display(Name = "Calle")]
        [MaxLength(60)]
        public string DomFis { get; set; }

        [Display(Name = "Número Exterior")]
        [MaxLength(30)]
        public string ExteriorFis { get; set; }

        [Display(Name = "Número Interior")]
        [MaxLength(30)]
        public string InteriorFis { get; set; }

        [Display(Name = "Colonia")]
        [MaxLength(50)]
        public string ColFis { get; set; }

        [Display(Name = "Código Postal")]
        [MaxLength(5)]
        public string CPFis { get; set; }

        [Display(Name = "Alcaldía o municipio")]
        [MaxLength(50)]
        public string DelFis { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(50)]
        public string CiudadFis { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(50)]
        public string EstadoFis { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(10)]
        public string TelFis { get; set; }


        #endregion

        #region Representante legal

        [Display(Name = "Nombre")]
        [MaxLength(50)]
        public string NombreRep { get; set; }

        [Display(Name = "Apellido Paterno")]
        [MaxLength(50)]
        public string PaternoRep { get; set; }

        [Display(Name = "Apellido Materno")]
        [MaxLength(50)]
        public string MaternoRep { get; set; }

        [Display(Name = "RFC")]
        [MinLength(12, ErrorMessage = "Longitud mínima de 12 caracteres.")]
        [MaxLength(13, ErrorMessage = "Longitud máxima de 13 caracteres.")]
        
        public string RFCRep { get; set; }

        [Display(Name = "CURP")]
        [MaxLength(18)]
        [RegularExpression("[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}" +
                            "(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])" +
                            "[HM]{1}" +
                            "(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)" +
                            "[B-DF-HJ-NP-TV-Z]{3}" +
                            "[0-9A-Z]{1}[0-9]{1}$", ErrorMessage = "Formato inválido.")]
        public string CURPRep { get; set; }

        #endregion

        #region Identificación representante legal

        public string TipIdentOtroMoral { get; set; }
        public string EmiteIdentMoral { get; set; }
        public string TipIdentOtroFisica { get; set; }
        public string EmiteIdentFisica { get; set; }

        [Display(Name = "Tipo de Identificación")]
        [MaxLength(200)]
        public string TipoIdentificacionRepresentanteAux { get; set; }//agregado

        [Display(Name = "Número de Identificación")]
        [MaxLength(200)]
        public string NumeroIdentificacionRepresentanteAux { get; set; }//agregado

        [Display(Name = "Fecha de constitución de la persona moral")]
        public DateTime FechaConstitucion { get; set; }

        //tipo numero
        #endregion

        #region Datos del poder

        [Display(Name = "Fecha poder")]
        public DateTime FechaPoder { get; set; }

        [Display(Name = "Número de instrumento")]
        [MaxLength(50)]
        public string NumeroInstrumento { get; set; }

        [Display(Name = "Número de notaría")]
        public int NumeroNotaria { get; set; }

        [Display(Name = "Estado de la notaría")]
        [MaxLength(70)]
        public string EstadoNotaria { get; set; }

        [Display(Name = "Notario")]
        [MaxLength(100)]
        public string Fedatario { get; set; }

        #endregion

        #region Poder notarial

        public DtPoderes dtPoderes { get; set; }

        #endregion

        #region Domicilio de correspondencia

        [Display(Name = "Emitir etiqueta")]
        public string ATTCamb { get; set; }

        //[Required(ErrorMessage = "Agregue valor")]
        [Display(Name = "Atención / Cambio de nombre de etiqueta")]
        public string EmiEti { get; set; }

        //falta notas

        #endregion

        #region Otros datos

        //[Required(ErrorMessage = "Agregue Banco")]
        [Display(Name = "Banco")]
        public int CveBan { get; set; }

        //[Required(ErrorMessage = "Agregue sucursal")]
        [Display(Name = "Sucursal")]
        [MaxLength(30)]
        public string SucDep { get; set; }

        //[Required(ErrorMessage = "Agregue cuenta")]
        [Display(Name = "Cuenta de depósito")]
        [MaxLength(220)]
        public string CtaDep { get; set; }

        //[Required(ErrorMessage = "Agregue fecha")]
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }

        //[Required(ErrorMessage = "Agregue fecha")]
        [Display(Name = "Fecha de baja")]
        public DateTime FechaBaja { get; set; }

        //[Required(ErrorMessage = "Agregue observaciones")]
        [Display(Name = "Observación")]
        [MaxLength(65535)]
        public string Observaciones { get; set; }

        #endregion

        [Display(Name = "Número de escritura")]
        [MaxLength(15)]
        public string NumeroEscritura { get; set; }
        [MaxLength(5)]
        [Display(Name = "Número de libro")]
        public string NumeroLibro { get; set; }
        [MaxLength(5)]
        [Display(Name = "Volumen")]
        public string Volumen { get; set; }
        [Display(Name = "Fecha de Poder")]
        public DateTime FechaPoderNotarial { get; set; }
        [Display(Name = "Nombre del Notario")]
        [MaxLength(100)]
        public string NombreNotario { get; set; }
        [Display(Name = "Número de Notario")]
        [MaxLength(5)]
        public string NumeroNotario { get; set; }
        [Display(Name = "Entidad Federativa")]
        [MaxLength(15)]
        public string EntidadFederativaNotario { get; set; }
        [Display(Name = "Fojas")]
        [MaxLength(5)]
        public string Fojas { get; set; }
        [Display(Name = "Nombre del Apoderado")]
        [MaxLength(100)]
        public string NombreApoderado { get; set; }



        string Representante { get; set; }
        string Usuario { get; set; }
        string Maquina { get; set; }
        string DireccionIP { get; set; }
        string ActividadEconomica { get; set; }
        char autorizarReparacion { get; set; }
        char IdTipoPropiedad { get; set; }
        public string Banco { get; set; }
        public string NacionalidadP { get; set; }
        [Display(Name = "Régimen Fiscal")]
        public int idRegimenFiscal { get; set; }
        [Display(Name = "Régimen Fiscal")]
        public string RegimenFiscal { get; set; }

    }
}

