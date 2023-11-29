var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
/* eslint-disable no-tabs */
import { useState, useEffect } from "react";
import "../scss/_header.scss";
import apiService from "../services/apiService"; // Ajustez le chemin vers votre fichier apiService
const Header = () => {
    const [sentences, setSentences] = useState([]); // État pour stocker les données des phrases
    useEffect(() => {
        // Fonction pour récupérer les phrases
        const fetchSentences = () => __awaiter(void 0, void 0, void 0, function* () {
            try {
                const response = yield apiService.getAllSentences();
                setSentences(response.data); // Mise à jour de l'état avec les données reçues
            }
            catch (error) {
                console.error("Erreur lors de la récupération des phrases:", error);
            }
        });
        fetchSentences(); // Appel de la fonction lors du chargement du composant
    }, []); // Le tableau vide signifie que l'effet ne s'exécute qu'au montage du composant
    return (_jsx("header", { className: "header", children: _jsxs("div", { className: "container", children: [_jsxs("div", { className: "row", children: [_jsxs("div", { className: "col", children: [_jsx("p", { children: "SYLVAIN BRETON" }), _jsx("div", { className: "underline" })] }), _jsx("div", { className: "col", children: _jsx("div", { className: "underline" }) }), _jsx("div", { className: "col", children: _jsx("div", { className: "underline" }) }), _jsxs("div", { className: "col", children: [_jsx("div", { className: "head-marquee", children: _jsx("div", { className: "scrolling-text", children: sentences.map((sentence, index) => (_jsx("em", { children: `"${sentence.content}" - ${sentence.author}, ${sentence.bookTitle}` }, index))) }) }), _jsx("div", { className: "underline" })] })] }), _jsx("div", { className: "row", children: _jsx("div", { className: "col", children: _jsx("p", { children: "Background : Don Brown" }) }) })] }) }));
};
export default Header;
//# sourceMappingURL=Header.js.map