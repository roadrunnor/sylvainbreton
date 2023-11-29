/* eslint-disable no-tabs */
import React, { useState, useEffect } from "react";
import "../scss/_header.scss";
import apiService from "../services/apiService"; // Ajustez le chemin vers votre fichier apiService
import { Sentence } from "../models/Sentence"; // Ajustez le chemin si nécessaire

const Header = () => {
	const [sentences, setSentences] = useState<Sentence[]>([]); // État pour stocker les données des phrases

	useEffect(() => {
		// Fonction pour récupérer les phrases
		const fetchSentences = async () => {
			try {
				const response = await apiService.getAllSentences();
				setSentences(response.data); // Mise à jour de l'état avec les données reçues
			} catch (error) {
				console.error("Erreur lors de la récupération des phrases:", error);
			}
		};

		fetchSentences(); // Appel de la fonction lors du chargement du composant
	}, []); // Le tableau vide signifie que l'effet ne s'exécute qu'au montage du composant

	return (
		<header className="header">
			<div className="container">
				<div className="row">
					<div className="col">
						<p>SYLVAIN BRETON</p>
						<div className="underline" />
					</div>
					<div className="col">
						<div className="underline" />
					</div>
					<div className="col">
						<div className="underline" />
					</div>
					<div className="col">
						<div className="head-marquee">
							<div className="scrolling-text">
								{sentences.map((sentence, index) => (
									<em key={index}>
										{`"${sentence.content}" - ${sentence.author}, ${sentence.bookTitle}`}
									</em>
								))}
							</div>
						</div>
						<div className="underline" />
					</div>
				</div>
				<div className="row">
					<div className="col">
						<p>Background : Don Brown</p>
					</div>
				</div>
			</div>
		</header>
	);
};

export default Header;
