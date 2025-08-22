import {useEffect, useRef, useState} from "react";
import {Text} from "@consta/uikit/Text";

import './header.css';
import {removeToken, removeUser} from "../../../services/auth.ts";
import type {UserInfo} from "../../../types/common-types.tsx";
import {User} from "@consta/uikit/User";
import {formatFio} from "../../../utils/string-utils.ts";

type MenuItem = {
    label: string;
    id: string;
};

export default function Header({userInfo, setUserInfo}: { userInfo: UserInfo | null, setUserInfo: (userInfo: UserInfo | null) => void }) {
    const menuItems: MenuItem[] = [
        {label: "Стенды", id: "stands"},
    ];

    const [activeItem, setActiveItem] = useState<string>(menuItems[0].id);
    const [isMenuOpen, setIsMenuOpen] = useState(false);

    const menuRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        function handleClickOutside(e: MouseEvent) {
            if (menuRef.current && !menuRef.current.contains(e.target as Node)) {
                setIsMenuOpen(false);
            }
        }

        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    return (
        <div className="header">
            <div className="logo">
                <Text size="l" weight="bold" key="logo">
                    PREDITRIX
                </Text>
            </div>

            {userInfo && (
                <div className="navigationMenu">
                    {menuItems.map((item) => (
                        <div
                            key={item.id}
                            className={`menuItem ${activeItem === item.id ? "active" : ""}`}
                            onClick={() => setActiveItem(item.id)}
                        >
                            <Text as="span" size="s">{item.label}</Text>
                        </div>
                    ))}
                </div>
            )}

            {userInfo && (
                <div
                    className="user"
                    ref={menuRef}
                    style={{position: "relative", cursor: "pointer"}}
                    onClick={() => setIsMenuOpen((prev) => !prev)}
                >
                    <User name={formatFio(userInfo.username)} info="Оператор" width={"full"} size={"l"} withArrow={true} />

                    {isMenuOpen && (
                        <div className="userDropdown">
                            <div
                                className="dropdownItem"
                                onClick={() => {
                                    removeToken();
                                    removeUser();
                                    setUserInfo(null);
                                    window.location.reload();
                                }}
                            >
                                Выйти
                            </div>
                        </div>
                    )}
                </div>
            )}
        </div>
    );
}


// const [underlineStyle, setUnderlineStyle] = React.useState({ left: 0, width: 0 });
// const menuRef = React.useRef<HTMLDivElement>(null);
//
// React.useEffect(() => {
//     if (!menuRef.current) return;
//     const activeEl = menuRef.current.querySelector('.menuItem.active') as HTMLElement | null;
//     if (activeEl) {
//         setUnderlineStyle({
//             left: activeEl.offsetLeft,
//             width: activeEl.offsetWidth,
//         });
//     }
// }, [activeItem]);

// <div className="navigationMenu" ref={menuRef}>
//     {menuItems.map((item) => (
//         <div
//             key={item.id}
//             className={`menuItem ${activeItem === item.id ? "active" : ""}`}
//             onClick={() => setActiveItem(item.id)}
//         >
//             {item.label}
//         </div>
//     ))}
//     <div className="underline" style={{ left: underlineStyle.left, width: underlineStyle.width }} />
// </div>


{/*<Avatar size="m" name={userInfo.username}/>*/}
{/*<div style={{display: "flex", flexDirection: "column"}}>*/}
{/*    <Text size="m">{formatFio(userInfo.username)}</Text>*/}
{/*    <Text size="s" view="secondary">Оператор</Text>*/}
{/*</div>*/}
