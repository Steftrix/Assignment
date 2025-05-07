# Aplicație pentru Gestionarea și Vizualizarea Voiajelor

Aceasta este o aplicație completă (frontend și backend) pentru gestionarea și vizualizarea datelor despre voiaje. Aplicația permite afișarea datelor într-un tabel, vizualizarea graficelor dinamice și filtrarea datelor pe baza anului selectat.


## Structura Proiectului

- **Frontend**: Aplicație Angular pentru interfața utilizatorului.
- **Backend**: API construit cu ASP.NET Core pentru gestionarea datelor și interogarea bazei de date PostgreSQL.



## Funcționalități

### Frontend
- **Tabel de Date**: Afișează informații despre voiaje cu coloane dinamice.
- **Grafic Bară**: Vizualizează numărul de plecări pe țară și an folosind `ngx-charts`.
- **Filtrare Dinamică**: Permite utilizatorilor să filtreze datele pe baza anului selectat dintr-un meniu dropdown.
- **Placeholder Dinamic**: Afișează un mesaj placeholder atunci când nu există date disponibile.
- **Design Responsiv**: Layout-ul se ajustează automat în funcție de prezența datelor.


### Backend
- **API REST**: Expune endpoint-uri pentru gestionarea datelor despre voiaje, țări, porturi și nave.
- **Interogări SQL Hardcodate**: Permite rularea interogărilor SQL direct din backend pentru performanță optimă.
- **Conexiune PostgreSQL**: Utilizează Npgsql pentru conectarea la baza de date PostgreSQL.