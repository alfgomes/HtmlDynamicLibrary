using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
	public class Sample : ICloneable
	{
		public long Id { get; set; }
		public string Brand { get; set; }
		public string EquipmentId { get; set; }
		public long InstalledBy { get; set; }
		public long? Quantity { get; set; }
		public string MensalValueStr { get; set; }
		public decimal? Progress { get; set; }
		public string MSISDN { get; set; }
		public string PhoneNumber { get; set; }
		public string Observations { get; set; }

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}



	[MetadataType(typeof(SampleMetadata))]
	public class SampleVM : Sample
	{

	}



	[Version(1, 0, 0, "26/07/2019", "Primeiro Teste")]
	[Version(1, 1, 0, "26/07/2019", "Segundo Teste")]
	public class SampleMetadata
	{
		[StringLength(120, ErrorMessage = "O valor Máximo é de 120 caracteres")]
		[Required(ErrorMessage = "Marca é obrigatória")]
		[Display(Name = "Marca", ShortName = "Marca", Description = "Indique a marca do veículo")]
		public string Brand { get; set; }

		[MinLength(5, ErrorMessage = "Devem ser informados no mínimo 5 caracteres")]
		[MaxLength(10, ErrorMessage = "Devem ser informados no máximo 10 caracteres")]
		[NoEspecialChars]
		[Display(Name = "ID Equipamento ", ShortName = "Equip. ID", Description = "A preencher pelo parceiro IoT")]
		public string EquipmentId { get; set; }

		[Required(ErrorMessage = "Instalação é obrigatório")]
		[Display(Name = "Instalação", ShortName = "Instalação", Description = "Selecione o responsável pela instalação")]
		public long InstalledBy { get; set; }

		[Range(0, 99999, ErrorMessage = "Máximo 5 dígitos")]
		[RegularExpression(@"^\d{0,5}", ErrorMessage = "Insira apenas valores numéricos")]
		[Required(ErrorMessage = "Quantidade é obrigatório")]
		[Display(Name = "Quantidade")]
		public long? Quantity { get; set; }

		[HtmlAttributes(Type = DataType.MultilineText, ID = "ID", Name = "Name", Class = "form-control", Style = "Estilos", Width = "300px")]
		[Required(ErrorMessage = "V. Mensal é obrigatório")]
		[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
		//[RegularExpression(@"\d{1,9}(\,{0,1}\d{0,2})?", ErrorMessage = "O valor tem de ser inserido com apenas 2 casas decimais, no formato:XXXXXXX,XX(com virgula)")]
		[Range(typeof(decimal), "0", "999999999,99", ErrorMessage = "O valor tem de estar entre 0 e 999999999,99")]
		[Currency]
		[Display(Name = "V. Mensal", ShortName = "V. Mensal", Description = "Indique um valor mensal para o plano")]
		public string MensalValueStr { get; set; }

		[ViewDisabled]
		[Display(Name = "MSISDN", ShortName = "MSISDN", Description = "A preencher pelo parceiro IoT")]
		[PlaceHolder("A preencher pelo parceiro IoT")]
		public string MSISDN { get; set; }

		[DataType(DataType.PhoneNumber, ErrorMessage = "Número de telefone inválido")]
		[RegularExpression(@"^\d*", ErrorMessage = "Insira apenas valores numéricos")]
		//[Range(0, 999999999, ErrorMessage = "Máximo 9 dígitos")]
		//[Range(typeof(decimal), "0", "10000000000000000000000000")]
		//[Required(ErrorMessage = "Nº Contacto é obrigatório")]
		[StringLength(25, ErrorMessage = "Máximo 25 dígitos")]
		[OnlyNumber]
		[Display(Name = "Contacto Telefónico", ShortName = "Telefone", Description = "Indique o contacto telefónico", GroupName = "Contacto do Cliente", Prompt = "Inserir valor")]
		public string PhoneNumber { get; set; }

		[Progress(300, Step = 3)]
		[Display(Name = "Avanço", ShortName = "Avanço", Description = "Percentual de Avanço do Projecto", GroupName = "Avanço Projecto")]
		public decimal? Progress { get; set; }

		[TextArea(6, 90, MaxLength = 600)]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Observações")]
		public string Observations { get; set; }
	}
}