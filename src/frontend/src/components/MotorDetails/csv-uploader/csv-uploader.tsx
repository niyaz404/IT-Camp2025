import React, { useState } from "react";
import { FileField } from "@consta/uikit/FileField";
import { Button } from "@consta/uikit/Button";
import { IconDocAdd } from "@consta/icons/IconDocAdd";

const CHUNK_SIZE = 5 * 1024 * 1024; // 5 MB
const API_URL = "http://localhost:8002/api/upload";

export default function CsvUploader() {
    const [progress, setProgress] = useState(0);

    const uploadFile = async (file: File) => {
        const totalChunks = Math.ceil(file.size / CHUNK_SIZE);

        for (let i = 0; i < totalChunks; i++) {
            const chunk = file.slice(i * CHUNK_SIZE, (i + 1) * CHUNK_SIZE);

            const formData = new FormData();
            formData.append("file", chunk);
            formData.append("fileName", file.name);
            formData.append("chunkIndex", i.toString());
            formData.append("totalChunks", totalChunks.toString());

            await fetch(API_URL, {
                method: "POST",
                body: formData,
            });

            setProgress(Math.round(((i + 1) / totalChunks) * 100));
        }

        alert("Загрузка завершена!");
    };

    const handleFileChange = (files: File[] | null) => {
        if (files?.length) {
            uploadFile(files[0]);
        }
    };

    return (
        <div>
            <FileField onChange={handleFileChange}>
                {(props) => (
                    <Button
                        iconLeft={IconDocAdd}
                        {...props}
                    />
                )}
            </FileField>
            <div>Прогресс: {progress}%</div>
        </div>
    );
}
