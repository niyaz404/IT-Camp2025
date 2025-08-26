import ComponentStatusCard from "../component-status-card/component-status-card";

import "./component-status-section.css"

const sampleData = [
    { x: "день1", y: 10 },
    { x: "день2", y: 11 },
    { x: "день3", y: 12 },
    { x: "день4", y: 12.4 },
];

export default function ComponentStatusSection() {
    return (
        <div className="componentStatusScroll">
            <div className="componentStatusRow">
                <ComponentStatusCard
                    title="Ротор"
                    value={12.4}
                    change={12.72}
                    chartData={sampleData}
                    defectFree
                />
                <ComponentStatusCard
                    title="Статор"
                    value={12.4}
                    change={12.72}
                    chartData={sampleData}
                />
                <ComponentStatusCard
                    title="Подшипники"
                    value={12.4}
                    change={12.72}
                    chartData={sampleData}
                />
                <ComponentStatusCard
                    title="Эксцентриситет"
                    value={13.7}
                    change={5.32}
                    chartData={sampleData}
                />
            </div>
        </div>
    );
}
