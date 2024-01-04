--
-- PostgreSQL database dump
--

-- Dumped from database version 15.3
-- Dumped by pg_dump version 15.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: hesap_hareketleri; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hesap_hareketleri (
    hareket_id bigint NOT NULL,
    hesap_id bigint,
    islem_tarihi date,
    islem_turu text,
    miktar bigint
);


ALTER TABLE public.hesap_hareketleri OWNER TO postgres;

--
-- Name: hesap_hareketleri_hareket_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.hesap_hareketleri ALTER COLUMN hareket_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.hesap_hareketleri_hareket_id_seq
    START WITH 500
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 5000
    CACHE 1
);


--
-- Name: hesap_no_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.hesap_no_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.hesap_no_seq OWNER TO postgres;

--
-- Name: hesapp; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hesapp (
    hesap_id bigint NOT NULL,
    musteri_id bigint NOT NULL,
    hesap_no character varying(30) DEFAULT ('H'::text || nextval('public.hesap_no_seq'::regclass)) NOT NULL,
    bakiye numeric NOT NULL
);


ALTER TABLE public.hesapp OWNER TO postgres;

--
-- Name: hesapp_hesap_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.hesapp_hesap_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.hesapp_hesap_id_seq OWNER TO postgres;

--
-- Name: hesapp_hesap_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.hesapp_hesap_id_seq OWNED BY public.hesapp.hesap_id;


--
-- Name: kullaniciler; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kullaniciler (
    kullanici_id bigint NOT NULL,
    kullaniciadi text NOT NULL,
    sifre bigint NOT NULL,
    musteri_id bigint NOT NULL
);


ALTER TABLE public.kullaniciler OWNER TO postgres;

--
-- Name: kullaniciler_kullanici_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.kullaniciler ALTER COLUMN kullanici_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.kullaniciler_kullanici_id_seq
    START WITH 10000
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 100000
    CACHE 1
);


--
-- Name: musteri; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.musteri (
    id bigint NOT NULL,
    ad text NOT NULL,
    soyad text NOT NULL,
    tck bigint NOT NULL
);


ALTER TABLE public.musteri OWNER TO postgres;

--
-- Name: musteri_İD_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.musteri ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."musteri_İD_seq"
    START WITH 10000
    INCREMENT BY 1
    MINVALUE 1000
    MAXVALUE 100000
    CACHE 1
);


--
-- Name: hesapp hesap_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hesapp ALTER COLUMN hesap_id SET DEFAULT nextval('public.hesapp_hesap_id_seq'::regclass);


--
-- Data for Name: hesap_hareketleri; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.hesap_hareketleri (hareket_id, hesap_id, islem_tarihi, islem_turu, miktar) FROM stdin;
501	2	2023-08-04	Para Yatırma	100
502	2	2023-08-04	Para Çekme	400
503	4	2023-08-04	Para Yatırma	1500
504	4	2023-08-04	Para Yatırma	500
505	2	2023-08-04	Para Yatırma	500
506	3	2023-08-04	Para Yatırma	750
507	2	2023-08-04	Para Çekme	400
508	4	2023-08-06	Para Çekme	15000
509	2	2023-08-06	Para Yatırma	10299
510	3	2023-08-06	Para Yatırma	500
511	7	2023-08-07	Para Yatırma	900
512	8	2023-08-07	Para Çekme	15000
513	5	2023-08-07	Para Yatırma	450
514	6	2023-08-07	Para Çekme	750
515	4	2023-08-07	Para Yatırma	500
516	5	2023-08-07	Para Yatırma	600
517	4	2023-08-10	Para Çekme	34
518	5	2023-08-10	Para Yatırma	234567
519	3	2023-08-14	Para Çekme	555
520	3	2023-08-14	Para Yatırma	555
521	5	2023-08-14	Para Yatırma	45
\.


--
-- Data for Name: hesapp; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.hesapp (hesap_id, musteri_id, hesap_no, bakiye) FROM stdin;
2	10050	H2	33195
7	10055	H7	18780
8	10056	H8	2924
6	10054	H6	24400
4	10052	H4	22909
3	10051	H3	28643
5	10053	H5	241991
11	10059	H11	355511
13	10061	H13	462137
1	10049	H1	27768
14	10065	H14	188983
15	10067	H15	221945
\.


--
-- Data for Name: kullaniciler; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.kullaniciler (kullanici_id, kullaniciadi, sifre, musteri_id) FROM stdin;
10004	srkndmrc	2612	10049
10006	nisanr	3456	10051
10007	hkndmr	1090	10052
10008	sdgrn	9870	10053
10009	ahmtcn	7021	10054
10010	rmysky	6090	10055
10011	alylmz	6512	10056
10013	emrdgdln	5409	10058
10014	cnsc	9999	10059
10015	hsncyln	1111	10060
10016	erdlakyldz	2345	10061
10005	ipeknr	2345	10050
10020	ipekr	1234	10065
10022	shnipek	3434	10067
\.


--
-- Data for Name: musteri; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.musteri (id, ad, soyad, tck) FROM stdin;
10050	ipek	öztürk	23456123123
10051	nisa	nur	90908989098
10052	hakan	demir	38987666676
10053	sude	gören	87656787656
10054	ahmet	can	77777778888
10055	rumeysa	kaya	22222888789
10056	ali	yılmaz	77765542112
10058	emir	dagdelen	27287277276
10059	can	sucu	43567899877
10060	hasan	ceylan	56765567656
10061	erdal	akyıldız	56654565457
10062	ipek 	öztürk	356772762611
10063	ipek	öztürk	2333444499
10064	ipek	öztürk	123457
10065	ipek	öztürk	224545498
10066	ipek	öztürk	2345456789
10067	ipek	şahin	45678905672
10049	serkan	demirci	19876514256
\.


--
-- Name: hesap_hareketleri_hareket_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hesap_hareketleri_hareket_id_seq', 524, true);


--
-- Name: hesap_no_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hesap_no_seq', 15, true);


--
-- Name: hesapp_hesap_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hesapp_hesap_id_seq', 15, true);


--
-- Name: kullaniciler_kullanici_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.kullaniciler_kullanici_id_seq', 10022, true);


--
-- Name: musteri_İD_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."musteri_İD_seq"', 10067, true);


--
-- Name: hesap_hareketleri hareket_id_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hesap_hareketleri
    ADD CONSTRAINT hareket_id_pk PRIMARY KEY (hareket_id);


--
-- Name: hesapp hesapp_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hesapp
    ADD CONSTRAINT hesapp_pkey PRIMARY KEY (hesap_id);


--
-- Name: kullaniciler kullaniciadi;_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kullaniciler
    ADD CONSTRAINT "kullaniciadi;_unique" UNIQUE (kullaniciadi) INCLUDE (kullaniciadi);


--
-- Name: kullaniciler kullaniciler_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kullaniciler
    ADD CONSTRAINT kullaniciler_pkey PRIMARY KEY (kullanici_id);


--
-- Name: musteri musteri_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.musteri
    ADD CONSTRAINT musteri_pkey PRIMARY KEY (id);


--
-- Name: fki_fk; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fk ON public.hesapp USING btree (hesap_id);


--
-- Name: fki_foreign_key; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_foreign_key ON public.musteri USING btree (id);


--
-- Name: fki_hesap_id_fk; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_hesap_id_fk ON public.hesap_hareketleri USING btree (hesap_id);


--
-- Name: fki_hesapp_id_fk; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_hesapp_id_fk ON public.hesap_hareketleri USING btree (hesap_id);


--
-- Name: fki_id_fk; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_id_fk ON public.musteri USING btree (id);


--
-- Name: fki_muster_id_fk; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_muster_id_fk ON public.hesapp USING btree (musteri_id);


--
-- Name: hesap_hareketleri hesap_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hesap_hareketleri
    ADD CONSTRAINT hesap_id_fk FOREIGN KEY (hesap_id) REFERENCES public.hesapp(hesap_id) NOT VALID;


--
-- Name: hesapp muster_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hesapp
    ADD CONSTRAINT muster_id_fk FOREIGN KEY (musteri_id) REFERENCES public.musteri(id) NOT VALID;


--
-- Name: kullaniciler musterii_id_Fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kullaniciler
    ADD CONSTRAINT "musterii_id_Fk" FOREIGN KEY (musteri_id) REFERENCES public.musteri(id) NOT VALID;


--
-- PostgreSQL database dump complete
--

