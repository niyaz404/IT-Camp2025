import ComponentStatusSection from "../component-status-section/component-status-section.tsx";
import {PhaseLoad} from "./../phase-load/phase-load.tsx";
import {Indicators} from "./../indicators/indicators.tsx";

export const Dashboard = () => {
    return (
        <div style={{padding: 24, display: "grid", gap: 24}}>
            <ComponentStatusSection/>
            <PhaseLoad/>
            <Indicators/>
        </div>
    );
};
