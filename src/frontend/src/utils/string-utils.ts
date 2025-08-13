export function formatFio(fio: string): string {
    if (!fio) return "";

    const parts = fio.trim().split(/\s+/); // Разделяем по пробелам
    if (parts.length === 1) {
        return parts[0];
    }
    if (parts.length === 2) {
        return `${parts[0]} ${parts[1][0]}.`;
    }
    return `${parts[0]} ${parts[1][0]}.${parts[2][0]}.`;
}