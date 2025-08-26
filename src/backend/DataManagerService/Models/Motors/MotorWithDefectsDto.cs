using DataManagerService.Models.Defects;
using DataManagerService.Models.Users;

namespace DataManagerService.Models.Motors;

/// <summary>
/// Расширенная сущность электродвигателя
/// </summary>
public class MotorWithDefectsDto
{
    /// <summary>
    /// Идентификатор экземпляра ЭД
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// FK на стенд (stands.id)
    /// </summary>
    public long StandId { get; set; }

    /// <summary>
    /// Производитель (может отличаться от модели)
    /// </summary>
    public string Manufacturer { get; set; }

    /// <summary>
    /// Заводской номер конкретного экземпляра ЭД
    /// </summary>
    public string FactoryNumber { get; set; }

    /// <summary>
    /// Дополнительное описание/примечания
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Текущее состояние (enum → 1 = В работе, 2 = Отключен и т.д.)
    /// </summary>
    public short State { get; set; }

    /// <summary>
    /// Дата установки/ввода в эксплуатацию
    /// </summary>
    public DateTime? InstalledAt { get; set; }
    
    /// <summary>
    /// Ответственный
    /// </summary>
    public KeycloakUserDto ResponsiblePerson { get; set; }

    /// <summary>
    /// Дата создания записи (UTC)
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата последнего обновления записи (UTC)
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Наименование модели/типа ЭД (например "ЭДС-110-117М")
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Номинальная мощность модели, кВт
    /// </summary>
    public decimal RatedPower { get; set; }

    /// <summary>
    /// Номинальная частота модели, Гц
    /// </summary>
    public double RatedFrequency { get; set; }

    /// <summary>
    /// Номинальная частота модели, Гц
    /// </summary>
    public double RatedCurrent { get; set; }

    /// <summary>
    /// Вольтаж
    /// </summary>
    public double Voltage { get; set; }

    /// <summary>
    /// Число фаз модели (обычно 3)
    /// </summary>
    public int PhasesCount { get; set; }

    /// <summary>
    /// Тип модели (1 = Асинхронный, 2 = Вентильный)
    /// </summary>
    public short Type { get; set; }
    
    /// <summary>
    /// Дефекты
    /// </summary>
    public IEnumerable<MotorDefectDto> Defects { get; set; }

    /// <summary>
    /// Максимальная степень дефекта (severity) по данному ЭД
    /// </summary>
    public int MaxSeverity { get; set; }
}