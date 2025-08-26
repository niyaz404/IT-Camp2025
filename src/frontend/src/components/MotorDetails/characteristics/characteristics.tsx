import { Card } from "@consta/uikit/Card";
import { Text } from "@consta/uikit/Text";

import "./characteristics.css";
import {type MotorDetails, MotorTypeMap} from "../../../types/common-types.tsx";

type CharacteristicsProps = {
    motor: MotorDetails;
};

export const Characteristics = ({ motor }: CharacteristicsProps) => {
    const characteristics = [
        { label: "Наименование", value: motor.name },
        { label: "Производитель", value: motor.manufacturer },
        { label: "Заводской номер", value: motor.factoryNumber },
        { label: "Описание", value: motor.description || "—" },
        { label: "Фазность", value: `${motor.phasesCount} фазы` },
        { label: "Частота", value: `${motor.ratedFrequency} Гц` },
        { label: "Мощность", value: `${motor.ratedPower} кВт` },
        { label: "Ток", value: `${motor.ratedCurrent} А` },
        { label: "Напряжение", value: `${motor.voltage} В` },
        { label: "Тип", value: MotorTypeMap[motor.type] },
    ];

    return (
        <Card className="characteristicsCard">
            <Text size="l" weight="bold" className="characteristicsTitle">
                Технические характеристики
            </Text>

            <div className="characteristicsGrid">
                {characteristics.map((item, idx) => (
                    <div key={idx} className="characteristicsRow">
                        <div className="characteristicsLabel">
                            <Text size="s" view="secondary">{item.label}</Text>
                        </div>
                        <div className="characteristicsValue">
                            <Text size="s">{item.value}</Text>
                        </div>
                    </div>
                ))}
            </div>
        </Card>
    );
};
