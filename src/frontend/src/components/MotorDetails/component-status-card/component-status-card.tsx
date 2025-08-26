import {Card} from "@consta/uikit/Card";
import {Text} from "@consta/uikit/Text";
import {Badge} from "@consta/uikit/Badge";
import {
    AreaChart,
    Area,
    ResponsiveContainer,
    CartesianGrid,
    XAxis,
    YAxis,
} from "recharts";

import "./component-status-card.css";

type ComponentStatusCardProps = {
    title: string;
    value: number;
    unit?: string;
    change: number;
    chartData: { x: string; y: number }[];
    defectFree?: boolean;
};

const ComponentStatusCard = ({
                                 title,
                                 value,
                                 unit = "R RMS, A",
                                 change,
                                 chartData,
                                 defectFree,
                             }: ComponentStatusCardProps) => {
    return (
        <Card className="componentCard">
            <div className="componentCardHeader">
                <Text size="m" weight="bold">
                    {title}
                </Text>
                {defectFree && <Badge status="success" label="ДЕФЕКТОВ НЕТ"/>}
                {!defectFree && (
                    <Text size="2xl" weight="bold">
                        {value}
                    </Text>)}
            </div>

            <div className="componentCardValue">
                <Text size="s" view="secondary">
                    {unit}
                </Text>
                <Text
                    size="s"
                    view={change >= 0 ? "success" : "alert"}
                    className="componentCardChange"
                >
                    {change >= 0 ? "+" : ""}
                    {change.toFixed(2)}%
                </Text>
            </div>
            <div className="componentCardChart">
                <ResponsiveContainer width="100%" height="100%">
                    <AreaChart data={chartData}>
                        <defs>
                            <linearGradient id="colorValue" x1="0" y1="0" x2="0" y2="1">
                                <stop offset="5%" stopColor="#4caf50" stopOpacity={0.3}/>
                                <stop offset="95%" stopColor="#4caf50" stopOpacity={0}/>
                            </linearGradient>
                        </defs>
                        <CartesianGrid strokeDasharray="3 3" vertical={false}/>
                        <XAxis dataKey="x" hide/>
                        <YAxis hide/>
                        <Area
                            type="monotone"
                            dataKey="y"
                            stroke="#4caf50"
                            fillOpacity={1}
                            fill="url(#colorValue)"
                        />
                    </AreaChart>
                </ResponsiveContainer>
            </div>
        </Card>
    );
};

export default ComponentStatusCard;
