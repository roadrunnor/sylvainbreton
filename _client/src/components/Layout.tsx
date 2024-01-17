import React, { useState, useEffect } from "react";
import "../scss/_layout.scss";
import { ImageData } from "../models/ImageData";
import { useApiService } from "../services/apiService"; // Importation de useApiService

const Layout = () => {
	const [images, setImages] = useState<ImageData[]>([]);
	const { apiService } = useApiService(); // Utilisation de useApiService pour accéder à apiService

	useEffect(() => {
		let isMounted = true; // Indicateur de montage du composant
		const fetchImages = async () => {
			try {
				// Utilisation de apiService.getAllImages pour récupérer les images
				const fetchedImages = await apiService.getAllImages();
				if (isMounted) {
					setImages(fetchedImages || []);
				}
			} catch (error) {
				// La gestion des erreurs est déjà gérée dans apiService
			}
		};

		fetchImages();
		// Nettoyage en cas de démontage du composant
		return () => {
			isMounted = false;
		};
	}, []); // Tableau de dépendances vide pour que l'effet ne s'exécute qu'au montage

	// You don't need the full filesystem path here, just the relative path from the public directory
	return (
		<div className="layout">
			{images.map((image) => (
				<img 
					key={image.ImageId}
					src={`${process.env.REACT_APP_IMAGE_PATH}${image.FileName}`}
					alt={image.Description}
				/>
			))}
		</div>
	);
};

export default Layout;
