CREATE TABLE countries (
    country_id SERIAL PRIMARY KEY,
    country_name VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE ports (
    port_id SERIAL PRIMARY KEY,
    port_name VARCHAR(255) NOT NULL,
    country_id INT REFERENCES countries(country_id)
);

CREATE TABLE ships (
    ship_id SERIAL PRIMARY KEY,
    ship_name VARCHAR(255) NOT NULL,
    max_speed DECIMAL(8,2) CHECK (max_speed > 0),
	ship_type VARCHAR(255) NOT NULL
);

CREATE TABLE voyages (
    voyage_id SERIAL PRIMARY KEY,
    ship_id INT REFERENCES ships(ship_id),
    departure_port INT REFERENCES ports(port_id),
    arrival_port INT REFERENCES ports(port_id),
    voyage_start TIMESTAMP NOT NULL,
    voyage_end TIMESTAMP NOT NULL,
    CHECK (voyage_end > voyage_start)
);

INSERT INTO countries (country_name) VALUES
('China'), ('United States'), ('Singapore'), ('Japan'), ('South Korea'),
('Germany'), ('Netherlands'), ('United Kingdom'), ('France'), ('Belgium'),
('Italy'), ('Spain'), ('Greece'), ('Norway'), ('Denmark'),
('Sweden'), ('Finland'), ('Poland'), ('Russia'), ('Turkey'),
('India'), ('Malaysia'), ('Thailand'), ('Vietnam'), ('Philippines'),
('Indonesia'), ('Australia'), ('New Zealand'), ('Canada'), ('Mexico'),
('Panama'), ('Brazil'), ('Argentina'), ('Chile'), ('Colombia'),
('South Africa'), ('Egypt'), ('Morocco'), ('Nigeria'), ('Saudi Arabia'),
('United Arab Emirates'), ('Qatar'), ('Oman'), ('Kuwait'), ('Israel'),
('Portugal'), ('Ireland'), ('Iceland'), ('Bangladesh'), ('Sri Lanka');

INSERT INTO ports (port_name, country_id) VALUES
('Port of Shanghai', 1), ('Port of Singapore', 3), ('Port of Busan', 5),
('Port of Hong Kong', 1), ('Port of Ningbo-Zhoushan', 1),
('Port of Shenzhen', 1), ('Port of Guangzhou', 1), ('Port of Qingdao', 1),
('Port of Tianjin', 1), ('Port of Kaohsiung', 1), ('Port of Tokyo', 4),
('Port of Yokohama', 4), ('Port of Osaka', 4), ('Port of Nagoya', 4),
('Port of Klang', 22), ('Port of Tanjung Pelepas', 22), ('Port of Laem Chabang', 23),
('Port of Ho Chi Minh City', 24), ('Port of Manila', 25), ('Port of Jakarta', 26),
('Port of Colombo', 50), ('Port of Chittagong', 49), ('Port of Dubai', 41),
('Port of Jebel Ali', 41), ('Port of Hazira', 21),
('Port of Rotterdam', 7), ('Port of Antwerp', 10), ('Port of Hamburg', 6),
('Port of Bremerhaven', 6), ('Port of Felixstowe', 8), ('Port of Southampton', 8),
('Port of Le Havre', 9), ('Port of Marseille', 9), ('Port of Valencia', 12),
('Port of Barcelona', 12), ('Port of Piraeus', 13), ('Port of Algeciras', 12),
('Port of Gothenburg', 16), ('Port of Aarhus', 15), ('Port of Gdansk', 18),
('Port of St. Petersburg', 19), ('Port of Istanbul', 20), ('Port of Izmir', 20),
('Port of Lisbon', 45), ('Port of Dublin', 47), ('Port of Oslo', 14),
('Port of Helsinki', 17), ('Port of Copenhagen', 15), ('Port of Zeebrugge', 10),
('Port of Genoa', 11),
('Port of Los Angeles', 2), ('Port of Long Beach', 2), ('Port of New York', 2),
('Port of Savannah', 2), ('Port of Houston', 2), ('Port of Vancouver', 29),
('Port of Prince Rupert', 29), ('Port of Manzanillo', 30), ('Port of Balboa', 31),
('Port of Colon', 31), ('Port of Santos', 32), ('Port of Rio de Janeiro', 32),
('Port of Buenos Aires', 33), ('Port of Valparaiso', 34), ('Port of Callao', 35),
('Port of Cartagena', 35), ('Port of Kingston', 2), ('Port of Freeport', 2),
('Port of Seattle', 2), ('Port of Tacoma', 2),
('Port of Durban', 36), ('Port of Cape Town', 36), ('Port of Alexandria', 37),
('Port of Tangier', 38), ('Port of Lagos', 39), ('Port of Jeddah', 40),
('Port of Dammam', 40), ('Port of Salalah', 43), ('Port of Haifa', 44),
('Port of Eilat', 44);

INSERT INTO ships (ship_name, max_speed, ship_type) VALUES
('MSC Gülsün', 22.5, 'Container'), ('Ever Ace', 22.0, 'Container'),
('CMA CGM Jacques Saadé', 22.0, 'Container'), ('HMM Algeciras', 22.0, 'Container'),
('COSCO Shipping Universe', 21.5, 'Container'), ('MSC Mina', 22.0, 'Container'),
('OOCL Hong Kong', 22.5, 'Container'), ('Madrid Maersk', 22.0, 'Container'),
('MOL Triumph', 22.0, 'Container'), ('Barzan', 22.5, 'Container'),
('MSC Oscar', 22.0, 'Container'), ('CMA CGM Antoine de Saint Exupery', 22.0, 'Container'),
('Ever Golden', 22.5, 'Container'), ('MOL Truth', 22.0, 'Container'),
('APL Esplanade', 21.5, 'Container'), ('MSC New York', 22.0, 'Container'),
('CMA CGM Marco Polo', 22.5, 'Container'), ('Ever Given', 22.5, 'Container'),
('MSC Zoe', 22.0, 'Container'), ('COSCO Development', 21.5, 'Container'),
('TI Europe', 16.5, 'Tanker'), ('Oceania', 16.0, 'Tanker'),
('Asia', 16.0, 'Tanker'), ('Africa', 16.5, 'Tanker'),
('Overseas Allyn', 17.0, 'Tanker'), ('Overseas Julie', 17.0, 'Tanker'),
('Overseas Marilyn', 17.0, 'Tanker'), ('Overseas Nikiski', 17.0, 'Tanker'),
('Seawise Giant', 16.0, 'Tanker'), ('Hellespont Fairfax', 16.5, 'Tanker'),
('Jahre Viking', 16.0, 'Tanker'), ('Pierre Guillaumat', 16.5, 'Tanker'),
('Batillus', 16.0, 'Tanker'), ('Bellamya', 16.5, 'Tanker'),
('Berri', 17.0, 'Tanker'),
('Symphony of the Seas', 22.0, 'Cruise'), ('Wonder of the Seas', 22.0, 'Cruise'),
('Harmony of the Seas', 22.0, 'Cruise'), ('Oasis of the Seas', 22.0, 'Cruise'),
('Allure of the Seas', 22.0, 'Cruise'), ('MSC Grandiosa', 21.5, 'Cruise'),
('Costa Smeralda', 21.5, 'Cruise'), ('AIDAnova', 21.5, 'Cruise'),
('Queen Mary 2', 26.0, 'Cruise'), ('Norwegian Bliss', 22.5, 'Cruise');

INSERT INTO voyages (ship_id, departure_port, arrival_port, voyage_start, voyage_end) VALUES
(1, 1, 3, '2016-03-15 08:00:00', '2016-03-22 17:00:00'), -- Shanghai to Busan
(2, 26, 1, '2016-04-10 12:00:00', '2016-04-25 09:00:00'), -- Rotterdam to Shanghai
(3, 5, 26, '2016-05-05 06:00:00', '2016-05-20 18:00:00'), -- Ningbo to Rotterdam
(4, 3, 5, '2016-06-12 14:00:00', '2016-06-15 09:00:00'), -- Busan to Ningbo
(5, 41, 3, '2016-07-01 10:00:00', '2016-07-08 16:00:00'), -- Dubai to Busan
(6, 2, 41, '2016-08-15 08:00:00', '2016-08-22 12:00:00'), -- Singapore to Dubai
(7, 26, 2, '2016-09-03 13:00:00', '2016-09-18 08:00:00'), -- Rotterdam to Singapore
(8, 1, 26, '2016-10-11 09:00:00', '2016-10-26 17:00:00'), -- Shanghai to Rotterdam
(9, 3, 1, '2016-11-07 11:00:00', '2016-11-14 14:00:00'), -- Busan to Shanghai
(10, 5, 3, '2016-12-01 07:00:00', '2016-12-06 12:00:00'), -- Ningbo to Busan
(11, 26, 5, '2016-01-20 14:00:00', '2016-02-05 09:00:00'), -- Rotterdam to Ningbo
(12, 41, 26, '2016-02-15 10:00:00', '2016-03-02 16:00:00'), -- Dubai to Rotterdam
(13, 2, 41, '2016-04-22 08:00:00', '2016-04-29 11:00:00'), -- Singapore to Dubai
(14, 1, 2, '2016-05-18 13:00:00', '2016-05-25 17:00:00'), -- Shanghai to Singapore
(15, 3, 1, '2016-06-09 09:00:00', '2016-06-16 14:00:00'), -- Busan to Shanghai
(16, 5, 3, '2016-07-25 07:00:00', '2016-07-30 12:00:00'), -- Ningbo to Busan
(17, 26, 5, '2016-08-14 14:00:00', '2016-08-29 09:00:00'), -- Rotterdam to Ningbo
(18, 41, 26, '2016-09-11 10:00:00', '2016-09-26 16:00:00'), -- Dubai to Rotterdam
(19, 2, 41, '2016-10-05 08:00:00', '2016-10-12 11:00:00'), -- Singapore to Dubai
(20, 1, 2, '2016-11-30 13:00:00', '2016-12-07 17:00:00'), -- Shanghai to Singapore
(21, 26, 1, '2017-01-10 09:00:00', '2017-01-25 17:00:00'), -- Rotterdam to Shanghai
(22, 3, 26, '2017-02-05 11:00:00', '2017-02-20 08:00:00'), -- Busan to Rotterdam
(23, 41, 3, '2017-03-12 07:00:00', '2017-03-19 12:00:00'), -- Dubai to Busan
(24, 2, 41, '2017-04-18 14:00:00', '2017-04-25 09:00:00'), -- Singapore to Dubai
(25, 5, 2, '2017-05-22 10:00:00', '2017-05-29 16:00:00'), -- Ningbo to Singapore
(26, 1, 5, '2017-06-15 08:00:00', '2017-06-22 11:00:00'), -- Shanghai to Ningbo
(27, 26, 1, '2017-07-03 13:00:00', '2017-07-18 17:00:00'), -- Rotterdam to Shanghai
(28, 3, 26, '2017-08-11 09:00:00', '2017-08-26 14:00:00'), -- Busan to Rotterdam
(29, 4, 3, '2017-09-07 07:00:00', '2017-09-12 12:00:00'), -- Hong Kong to Busan
(30, 6, 4, '2017-10-14 14:00:00', '2017-10-21 09:00:00'), -- Shenzhen to Hong Kong
(31, 7, 6, '2017-11-05 10:00:00', '2017-11-10 16:00:00'), -- Guangzhou to Shenzhen
(32, 8, 7, '2017-12-01 08:00:00', '2017-12-06 11:00:00'), -- Qingdao to Guangzhou
(33, 9, 8, '2017-01-20 13:00:00', '2017-01-25 17:00:00'), -- Tianjin to Qingdao
(34, 10, 9, '2017-02-15 09:00:00', '2017-02-20 14:00:00'), -- Kaohsiung to Tianjin
(35, 11, 10, '2017-03-22 07:00:00', '2017-03-27 12:00:00'), -- Tokyo to Kaohsiung
(36, 12, 11, '2017-04-18 14:00:00', '2017-04-23 09:00:00'), -- Yokohama to Tokyo
(37, 13, 12, '2017-05-11 10:00:00', '2017-05-16 16:00:00'), -- Osaka to Yokohama
(38, 14, 13, '2017-06-07 08:00:00', '2017-06-12 11:00:00'), -- Nagoya to Osaka
(39, 15, 14, '2017-07-05 13:00:00', '2017-07-10 17:00:00'), -- Klang to Nagoya
(40, 16, 15, '2017-08-12 09:00:00', '2017-08-17 14:00:00'), -- Tanjung Pelepas to Klang
(41, 17, 16, '2018-01-10 07:00:00', '2018-01-15 12:00:00'), -- Laem Chabang to Tanjung Pelepas
(42, 18, 17, '2018-02-05 14:00:00', '2018-02-10 09:00:00'), -- Ho Chi Minh to Laem Chabang
(43, 19, 18, '2018-03-12 10:00:00', '2018-03-17 16:00:00'), -- Manila to Ho Chi Minh
(44, 20, 19, '2018-04-18 08:00:00', '2018-04-23 11:00:00'), -- Jakarta to Manila
(45, 21, 20, '2018-05-22 13:00:00', '2018-05-27 17:00:00'); -- Colombo to Jakarta


-- Annual country visits materialized view
CREATE MATERIALIZED VIEW lastYearCountries AS
SELECT 
    c.country_name, 
    COUNT(*) AS visit_count,
    STRING_AGG(DISTINCT p.port_name, ', ' ORDER BY p.port_name) AS ports_visited
FROM voyages v
JOIN ports p ON v.arrival_port = p.port_id
JOIN countries c ON p.country_id = c.country_id
WHERE 
    voyage_end >= '2017-01-01 00:00:00' 
    AND voyage_end < '2018-01-01 00:00:00'
GROUP BY c.country_id, c.country_name
ORDER BY visit_count DESC;

CREATE INDEX idx_voyages_dates ON voyages USING BRIN(voyage_start, voyage_end);
CREATE INDEX idx_ships_type ON ships(ship_type);