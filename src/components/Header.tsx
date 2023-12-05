/* eslint-disable no-tabs */
import React, { useState, useEffect } from "react";
import "../scss/_header.scss";
import apiService from "../services/apiService"; // Ajustez le chemin vers votre fichier apiService
import { Sentence } from "../models/Sentence"; // Ajustez le chemin si nécessaire
import { Artist } from "../models/Artist"; // Adjust the path to wherever your Artist interface is defined

// Header.tsx
const Header = () => {
	const [artistName, setArtistName] = useState("");
	const [sentences, setSentences] = useState<Sentence[]>([]); // État pour stocker les données des phrases

	// Fetch artist data
	useEffect(() => {
		const findMyArtistEntry = async () => {
			try {
				const artists = await apiService.getAllArtists();
				// Check if artists is not undefined before calling find
				if (artists) {
					const myEntry = artists.find(
						(artist) =>
							artist.FirstName === "Sylvain" && artist.LastName === "Breton"
					);
					if (myEntry) {
						setArtistName(`${myEntry.FirstName} ${myEntry.LastName}`);
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
				console.error("Error fetching sentences:", error);
			}
		};
		fetchSentences(); // Appel de la fonction lors du chargement du composant
	}, []); // Le tableau vide signifie que l'effet ne s'exécute qu'au montage du composant

	return (
		<header className="header">
			<div className="container">
				<div className="row">
					<div className="col">
						{/* Dynamic artist name */}
						{artistName && <div className="uppercase-text">{artistName}</div>}
						{!artistName && <div className="uppercase-text">Artist Name</div>}
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
											<span className="uppercase-text">{sentence.content}</span>{" "}
											- {sentence.author}, {sentence.bookTitle}
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
