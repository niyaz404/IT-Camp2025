namespace WebApi.BLL.Models.Defects;

/// <summary>
/// Дефект электродвигателя
/// </summary>
public class MotorDefectModel
{
    /// <summary>
    /// Уникальный идентификатор дефекта
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// FK на двигатель (motors.id)
    /// </summary>
    public long MotorId { get; set; }

    /// <summary>
    /// Тип дефекта (1 = Статор, 2 = Ротор, 3 = Подшипники, 4 = Прочее)
    /// </summary>
    public short DefectType { get; set; }

    /// <summary>
    /// Степень выраженности дефекта (0–100)
    /// </summary>
    public short Severity { get; set; }

    /// <summary>
    /// Дата и время обнаружения (UTC)
    /// </summary>
    public DateTime DetectionDate { get; set; }

    /// <summary>
    /// Версия детектора, который нашёл дефект
    /// </summary>
    public string? DetectorVersion { get; set; }

    /// <summary>
    /// Дополнительная заметка по дефекту
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Дата создания записи (UTC)
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата последнего обновления записи (UTC)
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}