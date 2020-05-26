var map1 = function() {
	var key = {gender: this.gender};
	var value = this.weight;
	emit(key, value);
};

var reduce1 = function(key, values) {
	var count = 0;
	var totalW = 0;
	values.forEach(function(value) {
		count += 1;
		totalW += value;
	});
	return {avgWeight: (totalW/count).toFixed(2)};
};

db.dragons.mapReduce(
	map1,
	reduce1,
	{out: "gender_wise_average_weight" }
);

db.gender_wise_average_weight.find();