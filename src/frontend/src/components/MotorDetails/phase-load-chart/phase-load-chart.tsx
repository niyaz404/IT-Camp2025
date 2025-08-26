import { useState } from "react";
import { Line } from "@consta/charts/Line";
import { Button } from "@consta/uikit/Button";

const generateSinusoids = (maxX = 10, step = 0.1) => {
    const data: { time: number; value: number; phase: string }[] = [];

    for (let t = 0; t <= maxX; t += step) {
        data.push({ time: Number(t.toFixed(2)), value: Math.sin(t), phase: "R" });
        data.push({ time: Number(t.toFixed(2)), value: Math.sin(t - (2 * Math.PI) / 3), phase: "S" });
        data.push({ time: Number(t.toFixed(2)), value: Math.sin(t - (4 * Math.PI) / 3), phase: "T" });
    }

    return data;
};

export const PhaseLoadChart = () => {
    const allData = generateSinusoids(10, 0.05);
    const [activePhases, setActivePhases] = useState<string[]>(["R", "S", "T"]);

    const phases = ["Все", "R", "S", "T"];

    const filteredData =
        activePhases.length === 0 || activePhases.length === 3
            ? allData
            : allData.filter((d) => activePhases.includes(d.phase));

    const handlePhaseClick = (phase: string) => {
        if (phase === "Все") {
            setActivePhases(["R", "S", "T"]);
        } else {
            setActivePhases([phase]);
        }
    };

    return (
        <div>


            <Line
                data={filteredData}
                xField="time"
                yField="value"
                seriesField="phase"
                smooth
                height={300}
                xAxis={{
                    type: "linear",
                    tickInterval: 1,
                    min: 0,
                    max: 10,
                    title: { text: "Временные ряды фаз" },
                }}
                yAxis={{
                    title: { text: "Нагрузка" },
                }}
                legend={{
                    position: "top-left",
                    layout: "horizontal",     // горизонтально, но привязано к левому краю
                    marker: { symbol: "circle" },
                }}
                line={{
                    style: { lineWidth: 2 },
                }}
            />
        </div>
    );
};
