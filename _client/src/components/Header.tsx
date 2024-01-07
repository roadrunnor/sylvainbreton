/* eslint-disable no-tabs */
import React, { useState, useEffect } from "react";
import apiService, { useApiService } from "../services/apiService"; // Ajustez le chemin vers votre fichier apiService
import { Sentence } from "../models/Sentence"; // Ajustez le chemin si nécessaire
import "../scss/_header.scss";

// Header.tsx
const Header = () => {
	const [artistName, setArtistName] = useState("");
	const [sentences, setSentences] = useState<Sentence[]>([]); // État pour stocker les données des phrases
	const { apiService } = useApiService(); // Récupération du service API

	// Fetch artist data
	useEffect(() => {
		const findMyArtistEntry = async () => {
			try {
				const artists = await apiService.getAllArtists();

				if (artists) {
					const myEntry = artists.find(
						(artist) =>
							artist.FirstName === "Sylvain" && artist.LastName === "Breton"
					);

					if (myEntry) {
						setArtistName(`${myEntry.FirstName} ${myEntry.LastName}`);
					} else {
					}
				}
			} catch (error) {
				console.error("Error fetching artists:", error);
			}
		};

		findMyArtistEntry();
	}, []);

	useEffect(() => {
		const fetchSentences = async () => {
			try {
				const fetchedSentences = await apiService.getAllSentences();
				setSentences(fetchedSentences);
			} catch (error) {
				// Error handling is done inside apiService, so no need to set state here
			}
		};
		fetchSentences(); // Appel de la fonction lors du chargement du composant
	}, []); // Le tableau vide signifie que l'effet ne s'exécute qu'au montage du composant

	return (
		<header className="header">
			<div className="container">
				<div className="row">
					<img
						className="world-image"
						src={`${process.env.REACT_APP_IMAGE_PATH}/world.webp`}
						alt="World"
					/>
				</div>
				<div className="row">
					<div className="col">
						{artistName && <p className="uppercase-text">{artistName}</p>}
						{!artistName && <p className="uppercase-text">Artist Name</p>}
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
								{sentences.length > 0 ? (
									sentences.map((sentence, index) => (
										<em key={index}>
											<span className="uppercase-text">{sentence.Content}</span>
											- <span>{sentence.Author}</span>, {sentence.BookTitle},{" "}
											{sentence.Publisher}, p. {sentence.SentencePage},{" "}
											{sentence.CountryOfPublication},
											{sentence.CityOfPublication}
										</em>
									))
								) : (
									<p>No sentences available.</p>
								)}
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
