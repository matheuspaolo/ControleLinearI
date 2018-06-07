String instrucao_rx, valor1_str, valor2_str, valor3_str, str_temp;
int r1, r2, r3;
char d;

void setup() {
	Serial.begin(9600);
	Serial1.begin(4800);
	Serial.println("serial delimit test 1.0");
	pinMode(13, OUTPUT);
}

byte x = 0;

void loop() {

	if (Serial.available()) {
		char inst = Serial.read();
		if (inst == ',') {

			if (instrucao_rx == "receberv1") {
				str_temp = Serial.readString();
				valor1_str = str_temp;
				r1 = valor1_str.toInt();
				Serial.print("-- Valor de R1 recebido: ");
				Serial.print(valor1_str);
				Serial.println(" ohms");
				str_temp = "";

				Serial1.println("aehoo");

			}

			if (instrucao_rx == "receberv2") {
				str_temp = Serial.readString();
				valor2_str = str_temp;
				r2 = valor2_str.toInt();
				Serial.print("-- Valor de R2 recebido: ");
				Serial.print(valor2_str);
				Serial.println(" ohms");
				str_temp = "";
			}

			if (instrucao_rx == "receberv3") {
				str_temp = Serial.readString();
				valor3_str = str_temp;
				r3 = valor3_str.toInt();
				Serial.print("-- Valor de R3 recebido: ");
				Serial.print(valor3_str);
				Serial.println(" ohms\n");
				str_temp = "";
			}


			/////////
			instrucao_rx = "";
			/////////
		}
		else {
			instrucao_rx += inst;
		}
	}
}
