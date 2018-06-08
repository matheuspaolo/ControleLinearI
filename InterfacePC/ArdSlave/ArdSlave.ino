String comandoRx, sentido;
int velocidade;

void setup() {
	Serial.begin(9600);
	comandoRx = "velocidade_021";
}

void loop() {
	delay(500);

	if (comandoRx == "paolo") {
		digitalWrite(13, !digitalRead(13));
	}

	if (comandoRx.startsWith("velocidade")) {
		comandoRx.remove(0, 11);
		velocidade = comandoRx.toInt();
		Serial.print("A velocidade desejada foi: ");
		Serial.println(velocidade);
		////
		//// C�DIGO DE VELOCIDADE DO SHIELD
		////
		comandoRx = "";
	}

	if (comandoRx.startsWith("sentido")) {
		comandoRx.remove(0, 8);
		sentido = comandoRx;
		if (sentido == "horario") {
			////
			//// C�DIGO DE SENTIDO DO SHIELD
			////
			Serial.print("O sentido de rota��o escolhido foi: hor�rio");
		}
		else {
			////
			//// C�DIGO DE SENTIDO DO SHIELD
			////
			Serial.println("O sentido de rota��o escolhido foi: antihor�rio");
		}
		comandoRx = "";
	}
}